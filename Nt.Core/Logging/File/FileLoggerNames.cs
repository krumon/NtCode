namespace Nt.Core.Logging.File
{
    /// <summary>
    /// Reserved file names for the built-in file loggers.
    /// </summary>
    public static class FileLoggerNames
    {
        /// <summary>
        /// El archivo boot.log almacena la información relacionada con el estado del arranque del sistema.
        /// </summary>
        public const string Boot = "boot.log";

        /// <summary>
        /// Este archivo también es conocido como syslog y contiene la información diaria de como va funcionando nuestro sistema. Es un log de propósito general de varios servicios que utilizan este archivo. Este archivo es un buen lugar en donde iniciar a buscar errores cuando un problema se presenta.
        /// </summary>
        public const string Messages = "messages.log";

        /// <summary>
        /// Este archivo contiene logs relacionados a la seguridad. Cada vez que un usuario ejecuta comandos con un nivel superior, se almacena el comando ejecutado dentro del secure log. Si el usuario no puede completar un login, esta acción también es almacenada dentro de este archivo.
        /// </summary>
        public const string Secure = "secure.log";
    }
}
