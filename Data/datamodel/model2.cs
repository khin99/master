using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1.Data.datamodel
{
    public class model2
    {
        [Key]
        public int ExampleKey { get; set; }
        public int Model1Key { get; set; }
        public string Sentence { get; set; }
        public string InEnglish { get; set; }
        public string InMyanmar { get; set; }
        public int Status { get; set; } = 101;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;

        [ForeignKey("Model1Key")]
        public model1 model1Data{get;set;}
    }
}
