using OpenTK;
using OpenTK.Graphics;
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
    public class MiscBubbles : StoryboardObjectGenerator
    {
        public double BeatDuration;
        public override void Generate()
        {
            StoryboardLayer layer = GetLayer("Diff specific Layer");
            BeatDuration = Beatmap.TimingPoints.First().BeatDuration;

            MapperController mapperDisplay = new MapperController(this, layer, new Vector2(50, 360));

            mapperDisplay.InsertMapper("sb/lrc/mapper-chocomilku-.png", new Color4(191, 194, 245, 255));
            mapperDisplay.InsertMapper("sb/lrc/mapper-Chrisse.png", new Color4(154, 186, 252, 255));

            switch (Beatmap.Name)
            {
                case "Collab Insane":
                case "Collab Normal":
                    mapperDisplay.InitSprites(884, 0);
                    break;
                case "Collab Hard":
                case "Collab Easy":
                    mapperDisplay.InitSprites(884, 1);
                    break;

                default:
                    mapperDisplay.InitSprites(884, 0);
                    break;
            }

            for (int i = 1; i < Beatmap.Bookmarks.Count(); i++)
            {
                mapperDisplay.Next(Beatmap.Bookmarks.ToList()[i]);
            }

            mapperDisplay.FadeOut(224333);

            string diffImgPath = "sb/lrc/diff-in.png";
            Color4 diffColor = Color4.White;
            switch (Beatmap.Name)
            {
                case "Collab Insane":
                    diffImgPath = "sb/lrc/diff-in.png";
                    diffColor = new Color4(254, 149, 102, 255);
                    break;
                case "Collab Hard":
                    diffImgPath = "sb/lrc/diff-hd.png";
                    diffColor = new Color4(225, 243, 89, 255);
                    break;
                case "Collab Normal":
                    diffImgPath = "sb/lrc/diff-nm.png";
                    diffColor = new Color4(96, 255, 183, 255);
                    break;
                case "Collab Easy":
                    diffImgPath = "sb/lrc/diff-ez.png";
                    diffColor = new Color4(79, 208, 246, 255);
                    break;
            }

            OsbSprite diffSprite = layer.CreateSprite(diffImgPath, OsbOrigin.Centre, new Vector2(600, 360));
            diffSprite.Scale(884, 480.0 / GetMapsetBitmap(diffSprite.TexturePath).Height * 0.125);
            diffSprite.Color(884, diffColor);
            diffSprite.Fade(884, 1);
            diffSprite.Fade(224333, 0);
        }

        class MapperController(StoryboardObjectGenerator ctx, StoryboardLayer layer, Vector2 pos)
        {
            private readonly StoryboardObjectGenerator ctx = ctx;
            private readonly double BeatDuration = ctx.Beatmap.TimingPoints.First().BeatDuration;
            private readonly StoryboardLayer layer = layer;
            private readonly Vector2 pos = pos;
            private readonly List<OsbSprite> sprites = [];
            private readonly List<Color4> spriteColors = [];
            private int spriteIndex = -1;

            public void InsertMapper(string mapperImgPath, Color4 color)
            {
                OsbSprite sprite = layer.CreateSprite(mapperImgPath, OsbOrigin.Centre, pos);
                sprites.Add(sprite);
                spriteColors.Add(color);
            }

            public void InitSprites(int time, int startingIndex)
            {
                if (sprites.Count <= 0) throw new Exception("No mappers inserted");

                for (int i = 0; i < sprites.Count; i++)
                {
                    sprites[i].Scale(time, 854.0 / ctx.GetMapsetBitmap(sprites[i].TexturePath).Width * 0.175);
                    sprites[i].Color(time, spriteColors[i]);

                    if (i == startingIndex)
                    {
                        sprites[i].Fade(time, 1);
                        spriteIndex = startingIndex;
                    }
                    else
                    {
                        sprites[i].Fade(time, 0);
                    }
                }
            }

            public void Next(int time)
            {
                sprites[spriteIndex].Fade(OsbEasing.OutExpo, time - BeatDuration, time, 1, 0);
                sprites[spriteIndex].MoveX(OsbEasing.OutExpo, time - BeatDuration, time, pos.X, pos.X - 100);

                spriteIndex = (spriteIndex + 1) % sprites.Count;

                sprites[spriteIndex].Fade(OsbEasing.OutExpo, time - BeatDuration / 2, time + BeatDuration, 0, 1);
                sprites[spriteIndex].MoveX(OsbEasing.OutExpo, time - BeatDuration / 2, time + BeatDuration / 2, pos.X + 100, pos.X);
            }

            public void FadeOut(int time)
            {
                sprites[spriteIndex].Fade(time, 0);
            }

            // public void Next(int time, int index)
            // {
            //     sprites[spriteIndex].Fade(time - 200, time, 1, 0);

            //     spriteIndex = index;
            //     sprites[spriteIndex].Fade(time - 200, time, 0, 1);
            // }



        }
    }
}
