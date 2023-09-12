﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace VRCT
{
    public partial class MLModel3
    {
        /// <summary>
        /// model input class for MLModel3.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"Position")]
            public string Position { get; set; }

            [ColumnName(@"Hands")]
            public float Hands { get; set; }

            [ColumnName(@"Bodyparts")]
            public string Bodyparts { get; set; }

            [ColumnName(@"Injuries")]
            public string Injuries { get; set; }

            [ColumnName(@"Symptoms")]
            public string Symptoms { get; set; }

            [ColumnName(@"Goals")]
            public string Goals { get; set; }

            [ColumnName(@"Outcome")]
            public string Outcome { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for MLModel3.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"Position")]
            public float[] Position { get; set; }

            [ColumnName(@"Hands")]
            public float Hands { get; set; }

            [ColumnName(@"Bodyparts")]
            public float[] Bodyparts { get; set; }

            [ColumnName(@"Injuries")]
            public float[] Injuries { get; set; }

            [ColumnName(@"Symptoms")]
            public float[] Symptoms { get; set; }

            [ColumnName(@"Goals")]
            public float[] Goals { get; set; }

            [ColumnName(@"Outcome")]
            public uint Outcome { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public string PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }

        }

		#endregion

		public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine().Result, true);

		/// <summary>
		/// Use this method to predict on <see cref="ModelInput"/>.
		/// </summary>
		/// <param name="input">model input.</param>
		/// <returns><seealso cref=" ModelOutput"/></returns>
		public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            var prediction = predEngine.Predict(input);
			return prediction;
        }

        private static async Task<PredictionEngine<ModelInput, ModelOutput>> CreatePredictEngine()
		{
			const string filePath = "VRSM.zip";
			using var stream = await FileSystem.OpenAppPackageFileAsync(filePath);
			var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(stream, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}
