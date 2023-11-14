using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.Models
{
    [MetadataType(typeof(UsersMetadata))]
    public partial class Users
    {
        private class UsersMetadata
        {
            [Key]
            [Display(Name = "流水ID")]
            public int ID { get; set; }
            [Display(Name = "姓名")]
            public string UserName { get; set; }
            [Display(Name = "登入密碼")]
            public string UserPwd { get; set; }
            [Display(Name = "電子信箱")]
            public string UserEmail { get; set; }
        }
    }
}