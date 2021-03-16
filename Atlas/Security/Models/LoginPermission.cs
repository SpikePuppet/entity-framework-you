// // -----------------------------------------------------------------------
// // <copyright file="LoginPermission.cs">
// //     Copyright 2020 Clint Irving
// //     All rights reserved.
// // </copyright>
// // <author>Clint Irving</author>
// // -----------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Atlas.Security.Models
{
    public class LoginPermission
    {
        public bool? FullAccess { get; set; }

        [XmlIgnore] public virtual List<LoginPermissionItem> LoginPermissionItems { get; set; }
        
        public string Type { get; set; }

        [XmlIgnore] public virtual Login Login { get; set; }

        public int LoginId { get; set; }

        [Key] public int Id { get; set; }
    }
}