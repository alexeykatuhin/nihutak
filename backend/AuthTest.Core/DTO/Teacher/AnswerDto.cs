using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Core.DTO.Teacher
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SetAnswerDto
    {
        public List<AnswerDto> Answers { get; set; }
        public int OptionId { get; set; }
    }
}
