using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TestMakerFreeApi.ViewModels
{
    public class ResultViewModel
    {
        #region Constructor

        public ResultViewModel()
        {
        }

        #endregion

        #region Properties

        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Text { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public string Notes { get; set; }
        [DefaultValue(0)] public int Type { get; set; }
        [DefaultValue(0)] public int Flags { get; set; }
        [JsonIgnore] public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        #endregion
    }
}