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
    public class LyricBubbles : StoryboardObjectGenerator
    {
        public double BeatDuration;
        public override void Generate()
        {
            StoryboardLayer layer = GetLayer("LRC Layer");
            BeatDuration = Beatmap.TimingPoints.First().BeatDuration;

            // Chat
            Chat lrc0_01 = new Chat(this, $"{ConvertMsToTimeFormat(3126)} -- yaca", "sb/pfp/somunia.jpg", "sb/lrc/lrc0-01.png", layer, OsbOrigin.CentreLeft, new Vector2(485, 200), -12);
            lrc0_01.Fade(3126, 0.5f, 1);
            lrc0_01.Fade(14678, 0);

            // TODO: implement movement stuff

        }

        public string ConvertMsToTimeFormat(int milliseconds)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(milliseconds);
            return string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
        }

        class Chat
        {
            private readonly StoryboardObjectGenerator ctx;
            protected FontGenerator fontSmall;
            private readonly OsbSprite pfp;
            private readonly OsbSprite messageImg;
            private readonly List<OsbSprite> profileS;
            private readonly List<OsbSprite> sprites = [];
            public Chat(StoryboardObjectGenerator ctx, string profileName, string profilePath, string messageImgPath, StoryboardLayer layer, OsbOrigin origin, Vector2 spawnPos, float profilePosXOffset)
            {
                this.ctx = ctx;
                if (origin != OsbOrigin.CentreLeft && origin != OsbOrigin.CentreRight) throw new NotSupportedException("Only CentreLeft and CentreRight origins are supported.");

                // font 
                fontSmall = this.ctx.LoadFont("sb/fonts/fontSmall", new FontDescription()
                {
                    FontPath = "BestTen-CRT.otf",
                    FontSize = 25,
                    Color = Color4.White,
                },
                new FontOutline()
                {
                    Color = Color4.Black,
                    Thickness = 2
                }
                );

                // message
                messageImg = layer.CreateSprite(messageImgPath, origin, spawnPos);
                messageImg.Scale(0, 480.0f / this.ctx.GetMapsetBitmap(messageImg.TexturePath).Height * 0.075);
                messageImg.Fade(0, 0);

                // pfp
                Vector2 pfpPos = Vector2.Zero;
                if (origin == OsbOrigin.CentreRight) { pfpPos = new Vector2(spawnPos.X + 20, spawnPos.Y - 5); }
                else if (origin == OsbOrigin.CentreLeft) { pfpPos = new Vector2(spawnPos.X - 20, spawnPos.Y); }

                pfp = layer.CreateSprite(profilePath, OsbOrigin.Centre, pfpPos);
                pfp.Scale(0, 854.0 / this.ctx.GetMapsetBitmap(pfp.TexturePath).Width * 0.04);
                pfp.Fade(0, 0);

                var (lrcSize, offsets) = CalculateFontBaseWidth(fontSmall, profileName, origin);
                int d = origin == OsbOrigin.CentreLeft ? -1 : +1;

                // profile text
                Vector2 textOffset = new(this.ctx.GetMapsetBitmap(messageImg.TexturePath).Width, this.ctx.GetMapsetBitmap(messageImg.TexturePath).Height / 5);

                profileS = LyricSpritesFactoryHorizontal(layer, fontSmall, profileName, origin, new Vector2(spawnPos.X - (textOffset.X / (float)(Math.Floor((lrcSize.X + offsets.First()) / 10) - 1) * d) + profilePosXOffset, spawnPos.Y - textOffset.Y));

                sprites = [pfp, messageImg, .. profileS];
            }

            protected static List<OsbSprite> LyricLineSpriteFactoryHorizontal(StoryboardLayer layer, FontGenerator font, string lyric, OsbOrigin origin, Vector2 pos)
            {
                float fontScale = 0.5f;

                var texture = font.GetTexture(lyric);
                var position = new Vector2(pos.X - texture.BaseWidth * fontScale + texture.OffsetFor(origin).X * fontScale, pos.Y);

                var sprite = layer.CreateSprite(texture.Path, origin, position);
                sprite.Scale(0, fontScale);

                // for compatibility with other effects + to bring me less stress, i put the sprite in a list 
                return [sprite];
            }

            protected static (Vector2, List<float>) CalculateFontBaseWidth(FontGenerator font, string lyric, OsbOrigin origin)
            {
                float fontScale = 0.5f;

                float lineWidth = 0f;
                float lineHeight = 0f;
                List<float> offset = [];

                foreach (char letter in lyric)
                {
                    FontTexture texture = font.GetTexture(letter.ToString());

                    lineWidth += texture.BaseWidth * fontScale;
                    lineHeight = Math.Max(lineHeight, texture.BaseHeight * fontScale);
                    offset.Add(texture.OffsetFor(origin).X);
                }

                return (new Vector2(lineWidth, lineHeight), offset);
            }

            protected static List<OsbSprite> LyricSpritesFactoryHorizontal(StoryboardLayer layer, FontGenerator font, string lyric, OsbOrigin origin, Vector2 pos)
            {
                float letterX = pos.X;
                float letterY = pos.Y;
                float fontScale = 0.5f;

                float lineWidth = 0f;
                float lineHeight = 0f;

                foreach (char letter in lyric)
                {
                    FontTexture texture = font.GetTexture(letter.ToString());

                    lineWidth += texture.BaseWidth * fontScale;
                    lineHeight = Math.Max(lineHeight, texture.BaseHeight * fontScale);
                }

                List<OsbSprite> sprites = [];

                foreach (char letter in lyric)
                {
                    FontTexture texture = font.GetTexture(letter.ToString());

                    if (!texture.IsEmpty)
                    {
                        // Vector2 position = new Vector2(letterX, letterY) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                        Vector2 position = new(letterX - texture.BaseWidth / 4 + texture.OffsetFor(origin).X - lineWidth * fontScale, letterY);

                        OsbSprite sprite = layer.CreateSprite(texture.Path, origin, position);
                        sprite.Scale(0, fontScale);

                        sprites.Add(sprite);
                    }
                    letterX += texture.BaseWidth * fontScale;
                }
                return sprites;
            }

            public List<OsbSprite> GetSprites()
            {
                return sprites;
            }

            public void Fade(int time, double opacity) { foreach (OsbSprite sprite in sprites) { sprite.Fade(time, opacity); } }
            public void Fade(int startTime, int endTime, double opacity)
            {
                foreach (OsbSprite sprite in sprites)
                {
                    sprite.Fade(startTime, endTime, sprite.OpacityAt(startTime), opacity);
                }
            }
            public void Fade(int time, float snapDuration, double opacity)
            {
                int duration = (int)(snapDuration * ctx.Beatmap.TimingPoints.First().BeatDuration);
                foreach (OsbSprite sprite in sprites)
                {
                    sprite.Fade(time - duration, time, sprite.OpacityAt(time - duration), opacity);
                }
            }
        }
    }
}
