using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public int AnswerId { get; set; }
        public double? AnswerValue { get; set; }
    }
}
