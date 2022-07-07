﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Rulesets.Difficulty.Preprocessing;
using osu.Game.Rulesets.Difficulty.Skills;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Taiko.Difficulty.Evaluators;

namespace osu.Game.Rulesets.Taiko.Difficulty.Skills
{
    /// <summary>
    /// Calculates the colour coefficient of taiko difficulty.
    /// </summary>
    public class Colour : StrainDecaySkill
    {
        protected override double SkillMultiplier => 0.12;
        protected override double StrainDecayBase => 0.8;

        /// <summary>
        /// Applies a speed bonus dependent on the time since the last hit.
        /// </summary>
        /// <param name="interval">The interval between the current and previous note hit using the same key.</param>
        private static double speedBonus(double interval)
        {
            return Math.Pow(0.4, interval / 1000);
        }

        public Colour(Mod[] mods)
            : base(mods)
        {
        }

        protected override double StrainValueOf(DifficultyHitObject current)
        {
            double difficulty = ColourEvaluator.EvaluateDifficultyOf(current);
            return difficulty;
        }

        // TODO: Remove before pr
        // public static String GetDebugHeaderLabels()
        // {
        //     return "StartTime,Raw,Decayed,CoupledRunLength,RepetitionInterval,EncodingRunLength,Payload(MonoRunLength|MonoCount)";
        // }

        // // TODO: Remove before pr
        // public string GetDebugString(DifficultyHitObject current)
        // {
        //     double difficulty = ColourEvaluator.EvaluateDifficultyOf(current);
        //     difficulty *= speedBonus(current.DeltaTime);
        //     TaikoDifficultyHitObject? taikoCurrent = (TaikoDifficultyHitObject)current;
        //     TaikoDifficultyHitObjectColour? colour = taikoCurrent?.Colour;
        //     if (taikoCurrent != null && colour != null)
        //     {
        //         List<ColourEncoding> payload = colour.Encoding.Payload;
        //         string payloadDisplay = "";
        //         for (int i = 0; i < payload.Count; ++i)
        //         {
        //             payloadDisplay += $"({payload[i].Payload[0].RunLength}|{payload[i].Payload.Count})";
        //         }

        //         return $"{current.StartTime},{difficulty},{CurrentStrain},{colour.Encoding.Payload[0].Payload.Count},{colour.Encoding.RepetitionInterval},{colour.Encoding.Payload.Count},{payloadDisplay}";
        //     }
        //     else
        //     {
        //         return $"{current.StartTime},{difficulty},{CurrentStrain},0,0,0,0,0";
        //     }
        // }
    }
}
