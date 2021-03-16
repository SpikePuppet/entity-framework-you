﻿// // -----------------------------------------------------------------------
// // <copyright file="Login.cs">
// //     Copyright 2020 Clint Irving
// //     All rights reserved.
// // </copyright>
// // <author>Clint Irving</author>
// // -----------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Atlas.Model.Attributes;

namespace Atlas.Security.Models
{
    [AuditMe]
    public class Login
    {
        [XmlIgnore] 
        public virtual List<LoginPermission> LoginPermissions { get; set; }

        [DefaultValue(Value = "")]
        [Filter(AllowPartialStringMatch = true)]
        public string Username { get; set; }

        [DefaultValue(Value = "")]
        public string Email { get; set; }

        [DefaultValue(Value = "")]
        public string MobileNumber { get; set; }

        [XmlIgnore] 
        public virtual LoginReset LoginReset { get; set; }

        [NotMapped] 
        public List<string> Roles { get; set; }

        [NotMapped] 
        public bool LockedOut { get; set; }

        [NotMapped] 
        public string LastLockOutDate { get; set; }

        [NotMapped] 
        public string Password { get; set; }

        [NotMapped] 
        public string PasswordSalt { get; set; }

        [Key] 
        public int Id { get; set; }
    }
}