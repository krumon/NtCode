﻿using Nt.Core.Attributes;
using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Logging.Console
{
    internal sealed class PooledByteBufferWriter : IBufferWriter<byte>, IDisposable
    {
        private byte[] _rentedBuffer;
        private int _index;

        private const int MinimumBufferSize = 256;

        public PooledByteBufferWriter(int initialCapacity)
        {
            System.Diagnostics.Debug.Assert(initialCapacity > 0);

            _rentedBuffer = ArrayPool<byte>.Shared.Rent(initialCapacity);
            _index = 0;
        }

        public ReadOnlyMemory<byte> WrittenMemory
        {
            get
            {
                System.Diagnostics.Debug.Assert(_rentedBuffer != null);
                System.Diagnostics.Debug.Assert(_index <= _rentedBuffer.Length);
                return _rentedBuffer.AsMemory(0, _index);
            }
        }

        public int WrittenCount
        {
            get
            {
                System.Diagnostics.Debug.Assert(_rentedBuffer != null);
                return _index;
            }
        }

        public int Capacity
        {
            get
            {
                System.Diagnostics.Debug.Assert(_rentedBuffer != null);
                return _rentedBuffer.Length;
            }
        }

        public int FreeCapacity
        {
            get
            {
                System.Diagnostics.Debug.Assert(_rentedBuffer != null);
                return _rentedBuffer.Length - _index;
            }
        }

        public void Clear()
        {
            ClearHelper();
        }

        private void ClearHelper()
        {
            System.Diagnostics.Debug.Assert(_rentedBuffer != null);
            System.Diagnostics.Debug.Assert(_index <= _rentedBuffer.Length);

            _rentedBuffer.AsSpan(0, _index).Clear();
            _index = 0;
        }

        // Returns the rented buffer back to the pool
        public void Dispose()
        {
            if (_rentedBuffer == null)
            {
                return;
            }

            ClearHelper();
            byte[] toReturn = _rentedBuffer;
            _rentedBuffer = null;
            ArrayPool<byte>.Shared.Return(toReturn);
        }

        public void Advance(int count)
        {
            System.Diagnostics.Debug.Assert(_rentedBuffer != null);
            System.Diagnostics.Debug.Assert(count >= 0);
            System.Diagnostics.Debug.Assert(_index <= _rentedBuffer.Length - count);

            _index += count;
        }

        public Memory<byte> GetMemory(int sizeHint = 0)
        {
            CheckAndResizeBuffer(sizeHint);
            return _rentedBuffer.AsMemory(_index);
        }

        public Span<byte> GetSpan(int sizeHint = 0)
        {
            CheckAndResizeBuffer(sizeHint);
            return _rentedBuffer.AsSpan(_index);
        }

#if BUILDING_INBOX_LIBRARY
        internal ValueTask WriteToStreamAsync(Stream destination, CancellationToken cancellationToken)
        {
            return destination.WriteAsync(WrittenMemory, cancellationToken);
        }

        internal void WriteToStream(Stream destination)
        {
            destination.Write(WrittenMemory.Span);
        }
#else
        internal Task WriteToStreamAsync(Stream destination, CancellationToken cancellationToken)
        {
            return destination.WriteAsync(_rentedBuffer, 0, _index, cancellationToken);
        }

        internal void WriteToStream(Stream destination)
        {
            destination.Write(_rentedBuffer, 0, _index);
        }
#endif

        private void CheckAndResizeBuffer(int sizeHint)
        {
            System.Diagnostics.Debug.Assert(_rentedBuffer != null);
            System.Diagnostics.Debug.Assert(sizeHint >= 0);

            if (sizeHint == 0)
            {
                sizeHint = MinimumBufferSize;
            }

            int availableSpace = _rentedBuffer.Length - _index;

            if (sizeHint > availableSpace)
            {
                int currentLength = _rentedBuffer.Length;
                int growBy = Math.Max(sizeHint, currentLength);

                int newSize = currentLength + growBy;

                if ((uint)newSize > int.MaxValue)
                {
                    newSize = currentLength + sizeHint;
                    if ((uint)newSize > int.MaxValue)
                    {
                        ThrowHelper.ThrowOutOfMemoryException_BufferMaximumSizeExceeded((uint)newSize);
                    }
                }

                byte[] oldBuffer = _rentedBuffer;

                _rentedBuffer = ArrayPool<byte>.Shared.Rent(newSize);

                System.Diagnostics.Debug.Assert(oldBuffer.Length >= _index);
                System.Diagnostics.Debug.Assert(_rentedBuffer.Length >= _index);

                Span<byte> previousBuffer = oldBuffer.AsSpan(0, _index);
                previousBuffer.CopyTo(_rentedBuffer);
                previousBuffer.Clear();
                ArrayPool<byte>.Shared.Return(oldBuffer);
            }

            System.Diagnostics.Debug.Assert(_rentedBuffer.Length - _index > 0);
            System.Diagnostics.Debug.Assert(_rentedBuffer.Length - _index >= sizeHint);
        }
    }

    internal static partial class ThrowHelper
    {
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowOutOfMemoryException_BufferMaximumSizeExceeded(uint capacity)
        {
            throw new OutOfMemoryException($"Buffer Maximum Size Exceeded, {capacity}.");
        }
    }
}
