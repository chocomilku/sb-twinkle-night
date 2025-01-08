using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES10;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        double BeatDuration;

        public override void Generate()
        {
            BeatDuration = Beatmap.TimingPoints.First().BeatDuration;
            GetLayer("Remove BG").CreateSprite(Beatmap.BackgroundPath).Fade(0, 0);


            // Layers
            StoryboardLayer bgLayer = GetLayer("BG Layer");
            StoryboardLayer buildingLayer = GetLayer("Building Layer");

            // Sprites
            OsbSprite BgBackground = bgLayer.CreateSprite("sb/p.png");
            BgBackground.ScaleVec(0, 854, 480);
            BgBackground.Color(0, new Color4(5, 45, 84, 255));
            BgBackground.Fade(884, 1);
            BgBackground.Fade(AudioDuration, 0);

            // Building Loop
            // fg: 0, 0, 0 255,255,255
            // bg1: 37, 37, 37 132,132,132
            // bg2: 55, 55, 55 88,88,88
            // bg3: 70, 70, 70 83, 83, 83

            BuildingGenerator bg3Buidling = new(this, "sb/city loop bg3.png", buildingLayer, 435);
            bg3Buidling.LoopBuilding(884, (int)BeatDuration * 256, 224333);

            BuildingGenerator bg2Buidling = new(this, "sb/city loop bg2.png", buildingLayer, 460);
            bg2Buidling.LoopBuilding(884, (int)BeatDuration * 128, 224333);

            BuildingGenerator bg1Buidling = new(this, "sb/city loop bg1.png", buildingLayer, 485);
            bg1Buidling.LoopBuilding(884, (int)BeatDuration * 64, 224333);

            BuildingGenerator fgBuidling = new(this, "sb/city loop.png", buildingLayer, 510);
            fgBuidling.LoopBuilding(884, (int)BeatDuration * 16, 224333);


        }
        class BuildingGenerator()
        {
            private readonly StoryboardObjectGenerator ctx;
            private readonly OsbSprite sprite;
            private readonly OsbSprite sprite2;
            private readonly int startX;

            public BuildingGenerator(StoryboardObjectGenerator ctx, string path, StoryboardLayer layer, int yPos) : this()
            {
                float scale = 1.25f;
                this.ctx = ctx;
                this.startX = (int)(this.ctx.GetMapsetBitmap(path).Width / scale * 0.485);

                sprite = layer.CreateSprite(path, OsbOrigin.BottomCentre, new Vector2(startX, yPos));
                sprite.Scale(0, 854.0f / this.ctx.GetMapsetBitmap(sprite.TexturePath).Width * scale);
                sprite.Fade(0, 0);

                sprite2 = layer.CreateSprite(path, OsbOrigin.BottomCentre, new Vector2(startX, yPos));
                sprite2.Scale(0, 854.0f / this.ctx.GetMapsetBitmap(sprite2.TexturePath).Width * scale);
                sprite2.Fade(0, 0);

            }

            public void LoopBuilding(int startTime, int segmentLoopTime, int endTime)
            {
                sprite.Fade(startTime, 1);
                sprite2.Fade(startTime, 1);
                // sprite.Fade(endTime, 0);
                // sprite2.Fade(endTime, 0);

                int loopCount = endTime / segmentLoopTime;
                int delay = segmentLoopTime / 2;

                ctx.Log($"startTime: {startTime}, segmentLoopTime: {segmentLoopTime}, endTime: {endTime}, loopCount: {loopCount}, expected end time: {startTime + (segmentLoopTime * (loopCount + 1))}");


                sprite2.MoveX(OsbEasing.None, startTime, startTime + delay, startX / 4, -startX / 2);

                sprite.StartLoopGroup(startTime, loopCount);
                sprite.MoveX(OsbEasing.None, 0, segmentLoopTime, startX, -startX / 2);
                sprite.EndGroup();

                sprite2.StartLoopGroup(startTime + delay, loopCount);
                sprite2.MoveX(OsbEasing.None, 0, segmentLoopTime, startX, -startX / 2);
                sprite2.EndGroup();


            }

            public (OsbSprite, OsbSprite) GetSprites()
            {
                return (sprite, sprite2);
            }

        }

    }
}
