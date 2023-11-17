using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.Models
{
    [MetadataType(typeof(NotesMetadata))]
    public partial class Notes
    {
        private class NotesMetadata
        {
            [Key]
            [Display(Name = "流水ID")]
            public int ID { get; set; }
            [Display(Name = "筆記標題")]
            public string Title { get; set; }
            [Display(Name = "筆記內文")]
            public string Context { get; set; }
            [Display(Name = "筆記類別")]
            public string NodeType { get; set; }
        }
    }
}