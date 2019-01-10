// Copyright (c) 2019 Nicolás Alvarez <nicolas.alvarez@gmail.com>
// Licensed under the MIT license;
// see LICENSE.txt for details.

using System;
using System.Diagnostics;
using Monocle;
using Celeste;
using Microsoft.Xna.Framework;

namespace KillCounter
{
    [Tracked(false)]
    class CounterEntity: Entity
    {
        private Level level;
        private DeathsCounter counter;

        public CounterEntity(Level level)
        {
            Position = new Vector2(0f, 30f);
            Depth = -100;
            Tag = (Tags.HUD | Tags.Global);

            base.Add(this.counter = new DeathsCounter(AreaMode.Normal, false, 0));

            this.level = level;
        }
        public override void Update()
        {
            base.Update();
            AreaKey area = level.Session.Area;
            this.counter.Amount = SaveData.Instance.Areas_Safe[area.ID].Modes[(int)area.Mode].Deaths;
        }
    }
}
