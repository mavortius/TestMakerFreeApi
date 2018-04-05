using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TestMakerFreeApi.ViewModels
{
    public class QuestionViewModel
    {
        #region Constructor

        public QuestionViewModel()
        {
        }

        #endregion

        #region Properties

        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        [DefaultValue(0)] public int Type { get; set; }
        [DefaultValue(0)] public int Flags { get; set; }
        [JsonIgnore] public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        #endregion
    }
}