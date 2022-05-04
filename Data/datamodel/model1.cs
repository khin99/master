using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace project1.Data.datamodel
{
    public class model1
    {
        [Key]
        public int Key { get; set; }
        [Required]
        public string Vocab { get; set; }
        [Required]
        public string Romaji { get; set; }
        public string Hiragana { get; set; }
        public int OrderId { get; set; }
        public string InEnglish { get; set; }
        public string InMyanmar { get; set; }
        
        public string WordGroup { get; set; }
        public string GroupDetail { get; set; }
        public int Status { get; set; } = 101;
        public DateTime? CreatedDate { get; set; }=DateTime.Now;
        public DateTime? UpdatedDate { get; set; }=DateTime.Now;
        public IList<model2> Examples { get; set; }
    }
}