﻿using System;

namespace Nt.Core
{
    /// <summary>
    /// Options to create <see cref="SessionFilters"/> object.
    /// </summary>
    public class SessionFiltersOptions
    {

        #region Private members / default values

        /// <summary>
        /// Indicates if the ninjascript enter in historial data bars.
        /// </summary>
        private bool includeHistoricalData = true;

        /// <summary>
        /// Indicates if the ninjascript enter in partial holidays.
        /// </summary>
        private bool includePartialHolidays = true;

        /// <summary>
        /// Indicates if the ninjascript enter in partial holidays with late begin.
        /// </summary>
        private bool includeLateBegin = true;

        /// <summary>
        /// Indicates if the ninjascript enter in partial holidays with early end.
        /// </summary>
        private bool includeEarlyEnd = true;

        /// <summary>
        /// Represents the minimum date to enter with the ninjascript.
        /// </summary>
        private DateTime initialDate = SessionFilters.MIN_DATE;

        /// <summary>
        /// Represents the maximum date to enter with the ninjascript.
        /// </summary>
        private DateTime finalDate = SessionFilters.MAX_DATE;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets if the ninjascript enter in historial data bars.
        /// </summary>
        public bool IncludeHistoricalData => IncludeHistoricalData;

        /// <summary>
        /// Gets if the ninjascript enter in partial holidays.
        /// </summary>
        public bool IncludePartialHolidays 
        { 
            get => includePartialHolidays; 
            set 
            { 
                // Make sure value changed
                if (includePartialHolidays == value)
                    return;

                //Update values
                includePartialHolidays = value; 

                if (!value)
                {
                    includeEarlyEnd = value;
                    includeLateBegin = value;
                }
            } 
        }

        /// <summary>
        /// Gets if the ninjascript enter in partial holidays with late begin.
        /// </summary>
        public bool IncludeLateBegin 
        { 
            get => includeLateBegin; 
            set 
            {
                // Make sure value changed
                if (includeLateBegin == value)
                    return;

                //Update values
                includeLateBegin = value;

                if (value)
                    includePartialHolidays = value;
            } 
        }

        /// <summary>
        /// Gets if the ninjascript enter in partial holidays with early end.
        /// </summary>
        public bool IncludeEarlyEnd
        {
            get => includeEarlyEnd;
            set
            {
                // Make sure value changed
                if (includeEarlyEnd == value)
                    return;

                //Update values
                includeEarlyEnd = value;

                if (value)
                    includePartialHolidays = value;
            }
        }

        /// <summary>
        /// Gets the minimum date to enter with the ninjascript.
        /// </summary>
        public DateTime InitialDate { get { return initialDate; } private set { initialDate = value; } }

        /// <summary>
        /// Gets the maximum date to enter with the ninjascript.
        /// </summary>
        public DateTime FinalDate { get { return finalDate; } private set { finalDate = value; } }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to add the date filters.
        /// </summary>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <returns></returns>
        public SessionFiltersOptions AddDateFilters(DateTime initialDate, DateTime finalDate)
        {
            if(initialDate < finalDate)
            {
                this.initialDate = initialDate;
                this.finalDate = finalDate;
            }

            if (initialDate < SessionFilters.MIN_DATE)
                this.initialDate = SessionFilters.MIN_DATE;

            if (finalDate > SessionFilters.MAX_DATE)
                this.finalDate = SessionFilters.MAX_DATE;

            return this;

        }

        /// <summary>
        /// Mthod to add date filters.
        /// </summary>
        /// <param name="initialYear"></param>
        /// <param name="initialMonth"></param>
        /// <param name="initialDay"></param>
        /// <param name="finalYear"></param>
        /// <param name="finalMonth"></param>
        /// <param name="finalDay"></param>
        /// <returns></returns>
        public SessionFiltersOptions AddDateFilters(
            int initialYear = 1970, 
            int initialMonth = 1, 
            int initialDay = 1, 
            int finalYear = 2050, 
            int finalMonth = 12, 
            int finalDay = 31)
        {

            #region Make sure initial and final dates are correct

            if (initialMonth < 1 || initialMonth > 12)
                initialMonth = 1;

            if (finalMonth < 1 || finalMonth > 12)
                finalMonth = 12;

            if (initialDay < 1 || initialDay > 31)
                initialDay = 1;

            if (finalDay < 1 || finalDay > 31)
                finalDay = 31;

            if (initialDay > 28)
            {
                if (initialDay == 31)
                {
                    if (initialMonth == 4 || initialMonth == 6 || initialMonth == 9 || initialMonth == 11)
                        initialDay = 30;
                }

                if (initialMonth == 2 && initialDay > 28 && initialYear % 4 != 0)
                    initialDay = 28;
                else
                    initialDay = 29;
            }

            if (finalDay > 28)
            {
                if (finalDay == 31)
                {
                    if (finalMonth == 4 || finalMonth == 6 || finalMonth == 9 || finalMonth == 11)
                        finalDay = 30;
                }

                if (finalMonth == 2 && finalDay > 28 && finalYear % 4 != 0)
                    finalDay = 28;
                else
                    finalDay = 29;
            }

            #endregion

            AddDateFilters(new DateTime(initialYear, initialMonth, initialDay), new DateTime(finalYear, finalMonth, finalDay));

            return this;
        }

        /// <summary>
        /// Method to add date filters.
        /// </summary>
        /// <param name="initialYear"></param>
        /// <param name="initialMonth"></param>
        /// <param name="finalYear"></param>
        /// <param name="finalMonth"></param>
        /// <returns></returns>
        public SessionFiltersOptions AddDateFilters(
            int initialYear = 1970, 
            int initialMonth = 1, 
            int finalYear = 2050, 
            int finalMonth = 12)
        {
            AddDateFilters(initialYear,initialMonth,1,finalYear,finalMonth,31);

            return this;
        }

        /// <summary>
        /// Method to add date filters.
        /// </summary>
        /// <param name="initialYear"></param>
        /// <param name="finalYear"></param>
        /// <returns></returns>
        public SessionFiltersOptions AddDateFilters(
            int initialYear = 1970,
            int finalYear = 2050)
        {

            AddDateFilters(initialYear, 1, 1, finalYear, 12, 31);

            return this;
        }

        #endregion
    }
}