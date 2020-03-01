﻿using RimWorld;
using Verse;
using System.Collections.Generic;
using Harmony;
using System.Linq;

namespace TorannMagic
{
    [StaticConstructorOnStartup]
    class HediffComp_EnergyRegen : HediffComp
    {

        private bool initializing = true;
        private bool removeNow = false;

        public int duration = 10;

        public override void CompExposeData()
        {
            Scribe_Values.Look<int>(ref this.duration, "duration", 10, false);
            base.CompExposeData();
        }

        public string labelCap
        {
            get
            {
                return base.Def.LabelCap + (" x"+ this.parent.Severity.ToString("#.#"));
            }
        }

        public string label
        {
            get
            {
                return base.Def.label + (" x" + this.parent.Severity.ToString("#.#"));
            }
        }


        private void Initialize()
        {
            bool spawned = base.Pawn.Spawned;
            if (spawned)
            {

            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            bool flag = base.Pawn != null;
            if (flag)
            {
                if (initializing)
                {
                    initializing = false;
                    this.Initialize();
                }
            }
            if (Find.TickManager.TicksGame % 60 == 0)
            {
                this.duration--;
                if (this.duration <= 0)
                {
                    this.removeNow = true;
                }
            }
        }

        public override bool CompShouldRemove
        {
            get
            {
                return base.CompShouldRemove || this.removeNow || this.Pawn.story == null || this.Pawn.story.traits == null;
            }
        }

    }
}
