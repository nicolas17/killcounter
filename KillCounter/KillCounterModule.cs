// Copyright (c) 2019 Nicolás Alvarez <nicolas.alvarez@gmail.com>
// Licensed under the MIT license;
// see LICENSE.txt for details.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Celeste;
using Celeste.Mod;

namespace KillCounter
{
    public class KillCounterModule : EverestModule
    {
        public override Type SettingsType => null;

        public override void Load()
        {
            Debug.WriteLine("Load called");
            On.Celeste.Level.LoadLevel += OnLoadLevel;
            Everest.Events.Level.OnExit += OnExit;
        }

        private void OnExit(Level level, LevelExit exit, LevelExit.Mode mode, Session session, HiresSnow snow)
        {
            Debug.WriteLine($"OnExit({level}, {exit}, {mode}, {session}, {snow})");
        }

        private void OnLoadLevel(On.Celeste.Level.orig_LoadLevel orig, Celeste.Level level, Celeste.Player.IntroTypes playerIntro, bool isFromLoader)
        {
            orig(level, playerIntro, isFromLoader);
            Debug.WriteLine($"OnLoadLevel called, intro type {playerIntro}, isFromLoader {isFromLoader}");
            if (isFromLoader) {
                List<Monocle.Entity> e = level.Tracker.GetEntities<CounterEntity>();
                if (e.Count == 1) {
                    Debug.WriteLine("counter already exists");
                } else {
                    Debug.WriteLine("Creating counter entity");
                    level.Add(new CounterEntity(level));
                }
            }
        }

        public override void Unload()
        {
            Debug.WriteLine("Unload called");
        }
    }
}
