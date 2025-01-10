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
using System.Security.Cryptography.X509Certificates;

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
            StoryboardLayer celestialLayer = GetLayer("Celestial Bodies Layer");
            StoryboardLayer blankLayer = GetLayer("Blank Screen Layer");
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

            StarGenerator("sb/city/star.png", celestialLayer, 884, 224333, 10, 1, 1.5f);

            OsbSprite moon = celestialLayer.CreateSprite("sb/city/moon.png", OsbOrigin.Centre, new Vector2(75, 115));
            moon.Fade(884, 1);
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
            bg3Buidling.LoopBuilding(884, (int)BeatDuration * 160, 224333);

            BuildingGenerator bg2Buidling = new(this, "sb/city/city loop bg2.png", buildingLayer2, 460);
            bg2Buidling.InitSprites(884);
            bg2Buidling.LoopBuilding(884, (int)BeatDuration * 81, 224333);

            BuildingGenerator bg1Buidling = new(this, "sb/city/city loop bg1.png", buildingLayer1, 485);
            bg1Buidling.InitSprites(884);
            bg1Buidling.LoopBuilding(884, (int)BeatDuration * 50, 224333);

            BuildingGenerator fgBuidling = new(this, "sb/city/city loop.png", buildingLayer, 510);
            fgBuidling.InitSprites(884);
            fgBuidling.LoopBuilding(884, (int)BeatDuration * 13, 224333);

            // Blank Screens
            BlankScreenController blank = new BlankScreenController("sb/p.png", blankLayer);
            blank.InitializeBlankScreen(884, 3643, 1);
            blank.ChangeScale(884, BeatDuration * 20, 0);

            blank.InitializeBlankScreen(213298, 224333, 0);
            blank.ChangeScale(213298, BeatDuration * 28, 0.5, OsbEasing.OutSine);
            blank.ChangeScale(222953, BeatDuration * 4, 1, OsbEasing.InExpo);

            // BG Flash
            BlankFlash flash = new BlankFlash(this, GetLayer("Blank Flash Layer"), "sb/p.png", new Color4(75, 148, 219, 255));
            flash.InitSprite(3643);

            flash.FlashTrigger(884, 224333, "HitSoundNormalFinish", 8f);

            // flash.Flash(3643, 8f);
            // flash.Flash(9160, 8f);
            // flash.Flash(14678, 8f);
            // flash.Flash(25712, 8f);

        }

        void StarGenerator(string path, StoryboardLayer layer, int startTime, int endTime, int amount, float initialScale, float maxScale)
        {
            int timeDuration = endTime - startTime;
            for (int i = 0; i < amount; i++)
            {
                float X = Random(180, 700);
                float Y = Random(OsuHitObject.WidescreenStoryboardBounds.Top + OsuHitObject.PlayfieldToStoryboardOffset.Y, OsuHitObject.WidescreenStoryboardBounds.Bottom / 2.5f);

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

        class BlankScreenController()
        {
            private readonly OsbSprite spriteTop;
            private readonly OsbSprite spriteBottom;
            public BlankScreenController(string pPath, StoryboardLayer layer) : this()
            {
                spriteTop = layer.CreateSprite(pPath, OsbOrigin.TopCentre, new Vector2(0, 0));
                spriteBottom = layer.CreateSprite(pPath, OsbOrigin.BottomCentre, new Vector2(0, 480));
            }

            // blank screen thingy is supposed to be scalable with two instance of them for up and down + rotation controller too would be nice
            public void InitializeBlankScreen(double startTime, double endTime, double initialScale = 1f)
            {

                spriteTop.Fade(startTime, endTime, 1, 1);
                spriteBottom.Fade(startTime, endTime, 1, 1);
                double relativeScale = initialScale * (480.0f / 4);

                spriteTop.ScaleVec(startTime, 854.0f, relativeScale);
                spriteBottom.ScaleVec(startTime, 854.0f, relativeScale);

                spriteTop.Color(startTime, Color4.Black);
                spriteBottom.Color(startTime, Color4.Black);
            }

            public void ChangeScale(int startTime, double duration, double newScale, OsbEasing easing = OsbEasing.OutExpo)
            {
                double relativeScale = newScale * (480.0f / 4);

                spriteTop.ScaleVec(easing, startTime, startTime + duration, spriteTop.ScaleAt(startTime), 854.0f, relativeScale);

                spriteBottom.ScaleVec(easing, startTime, startTime + duration, spriteBottom.ScaleAt(startTime), 854.0f, relativeScale);
            }

        }

        class BlankFlash
        {
            private readonly StoryboardObjectGenerator ctx;
            private readonly OsbSprite sprite;
            private readonly Color4 color;

            public BlankFlash(StoryboardObjectGenerator ctx, StoryboardLayer layer, string path, Color4 color)
            {
                this.ctx = ctx;
                this.color = color;

                sprite = layer.CreateSprite(path, OsbOrigin.Centre, new Vector2(320, 240));
            }

            public void InitSprite(int time)
            {
                sprite.ScaleVec(time, 854, 480);
                sprite.Color(time, color);
                sprite.Additive(time);
            }

            public void FlashTrigger(int startTime, int endTime, string trigger, float snapDuration, double opacity = 0.25)
            {
                int duration = (int)(snapDuration * ctx.Beatmap.TimingPoints.First().BeatDuration);

                sprite.StartTriggerGroup(trigger, startTime, endTime);
                int time = 0;

                sprite.Fade(OsbEasing.In, time - ctx.Beatmap.TimingPoints.First().BeatDuration, time, 0, opacity);
                sprite.Fade(OsbEasing.OutExpo, time, time + duration, opacity, 0);
                sprite.EndGroup();
            }

            public void Flash(int time, float snapDuration, double opacity = 0.25)
            {
                int duration = (int)(snapDuration * ctx.Beatmap.TimingPoints.First().BeatDuration);

                sprite.Fade(OsbEasing.In, time - ctx.Beatmap.TimingPoints.First().BeatDuration, time, 0, opacity);
                sprite.Fade(OsbEasing.OutExpo, time, time + duration, opacity, 0);

            }

        }

    }
}
