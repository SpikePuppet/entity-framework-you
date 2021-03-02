// // -----------------------------------------------------------------------
// // <copyright file="DateTimeRange.cs">
// //     Copyright 2020 Clint Irving
// //     All rights reserved.
// // </copyright>
// // <author>Clint Irving</author>
// // -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Atlas.Model.FilterExtensions
{
    public class DateTimeRange
    {
        public DateTimeRange()
        {
            After = DateTime.MinValue;
            Before = new DateTime(9999, 12, 31, 23, 59, 59);
        }

        public DateTime After { get; set; }
        public DateTime Before { get; set; }
    }
}