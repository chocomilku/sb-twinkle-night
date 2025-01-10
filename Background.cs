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
            StoryboardLayer buildingLayer = GetLayer("Building Layer FG");
            StoryboardLayer buildingLayer1 = GetLayer("Building Layer BG1");
            StoryboardLayer buildingLayer2 = GetLayer("Building Layer BG2");
            StoryboardLayer buildingLayer3 = GetLayer("Building Layer BG3");

            // Sprites
            OsbSprite BgBackground = bgLayer.CreateSprite("sb/p.png");
            BgBackground.ScaleVec(884, 854, 480);
            BgBackground.Color(884, new Color4(5, 45, 84, 255));
            BgBackground.Fade(884, 1);
            BgBackground.Fade(224333, 0);

            StarGenerator("sb/city/star.png", bgLayer, 884, 224333, 5, 1, 1.5f);

            OsbSprite moon = bgLayer.CreateSprite("sb/city/moon.png", OsbOrigin.Centre, new Vector2(75, 115));
            moon.Fade(884, 0.6);
            moon.Fade(224333, 0);
            moon.Scale(884, 854.0f / GetMapsetBitmap(moon.TexturePath).Width * 0.25);
            moon.MoveX(884, 224333, moon.PositionAt(884).X, 0);
            moon.Additive(884);

            // Building Loop
            // fg: 0, 0, 0 255,255,255
            // bg1: 37, 37, 37 132,132,132
            // bg2: 55, 55, 55 88,88,88
            // bg3: 70, 70, 70 83, 83, 83

            BuildingGenerator bg3Buidling = new(this, "sb/city/city loop bg3.png", buildingLayer3, 435);
            bg3Buidling.InitSprites(884);
            bg3Buidling.LoopBuilding(884, (int)BeatDuration * 256, 224333);

            BuildingGenerator bg2Buidling = new(this, "sb/city/city loop bg2.png", buildingLayer2, 460);
            bg2Buidling.InitSprites(884);
            bg2Buidling.LoopBuilding(884, (int)BeatDuration * 128, 224333);

            BuildingGenerator bg1Buidling = new(this, "sb/city/city loop bg1.png", buildingLayer1, 485);
            bg1Buidling.InitSprites(884);
            bg1Buidling.LoopBuilding(884, (int)BeatDuration * 64, 224333);

            BuildingGenerator fgBuidling = new(this, "sb/city/city loop.png", buildingLayer, 510);
            fgBuidling.InitSprites(884);
            fgBuidling.LoopBuilding(884, (int)BeatDuration * 13, 224333);

        }

        void StarGenerator(string path, StoryboardLayer layer, int startTime, int endTime, int amount, float initialScale, float maxScale)
        {
            int timeDuration = endTime - startTime;
            for (int i = 0; i < amount; i++)
            {
                float X = Random(140, 425);
                float Y = Random(OsuHitObject.WidescreenStoryboardBounds.Top + OsuHitObject.PlayfieldToStoryboardOffset.Y, OsuHitObject.WidescreenStoryboardBounds.Bottom / 2);

                OsbSprite star = layer.CreateSprite(path, OsbOrigin.Centre, new Vector2(X, Y));

                star.Fade(startTime, 0.9);
                // star.Additive(startTime);
                star.Fade(endTime, 0);

                double scale = 854.0f / GetMapsetBitmap(star.TexturePath).Width * 0.05 * initialScale;
                star.Scale(startTime, scale);
                star.MoveX(startTime, endTime, star.PositionAt(startTime).X, star.PositionAt(startTime).X - 75);

                int loopCount = Random(7, 27);
                int loopDuration = timeDuration / loopCount;
                int randomRotate = Random(-45, 45);

                star.StartLoopGroup(startTime, loopCount);
                star.Rotate(OsbEasing.InOutSine, 0, loopDuration / 4 * 1, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(randomRotate));
                star.Rotate(OsbEasing.InOutSine, loopDuration / 4 * 1, loopDuration / 4 * 2, MathHelper.DegreesToRadians(randomRotate), MathHelper.DegreesToRadians(0));
                star.Rotate(OsbEasing.InOutSine, loopDuration / 4 * 2, loopDuration / 4 * 3, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(-randomRotate));
                star.Rotate(OsbEasing.InOutSine, loopDuration / 4 * 3, loopDuration / 4 * 4, MathHelper.DegreesToRadians(-randomRotate), MathHelper.DegreesToRadians(0));


                star.Scale(OsbEasing.InExpo, loopDuration / 4 * 0, loopDuration / 4 * 1, scale, scale * maxScale);
                star.Scale(OsbEasing.OutExpo, loopDuration / 4 * 1, loopDuration / 4 * 2, scale * maxScale, scale);

                star.Scale(OsbEasing.InExpo, loopDuration / 4 * 2, loopDuration / 4 * 3, scale, scale * maxScale);
                star.Scale(OsbEasing.OutExpo, loopDuration / 4 * 3, loopDuration / 4 * 4, scale * maxScale, scale);
                star.EndGroup();
            }
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
                startX = (int)(this.ctx.GetMapsetBitmap(path).Width / scale * 0.485);

                sprite = layer.CreateSprite(path, OsbOrigin.BottomCentre, new Vector2(startX, yPos));
                sprite2 = layer.CreateSprite(path, OsbOrigin.BottomCentre, new Vector2(startX, yPos));
            }

            public void InitSprites(int time)
            {
                float scale = 1.25f;

                sprite.Scale(time, 854.0f / ctx.GetMapsetBitmap(sprite.TexturePath).Width * scale);
                sprite2.Scale(time, 854.0f / ctx.GetMapsetBitmap(sprite2.TexturePath).Width * scale);

                sprite.Fade(time, 1);
                sprite2.Fade(time, 1);
            }

            public void LoopBuilding(int startTime, int segmentLoopTime, int endTime)
            {
                sprite.Fade(endTime, 0);
                sprite2.Fade(endTime, 0);

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
