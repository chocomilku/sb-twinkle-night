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

            // Font
            FontGenerator fontSmall = LoadFont("sb/fonts/fontSmall", new FontDescription()
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

            // Chat

            ChatController osuChat = new ChatController(this, layer, fontSmall, new Vector2(485, 260), new Vector2(90, 260));

            // somunia - hook
            osuChat.InsertLine(3126, "somunia", "lrc0-01", OsbOrigin.CentreRight, new Vector2(-26, 0));
            osuChat.InsertLine(5540, "somunia", "lrc0-02", OsbOrigin.CentreRight, new Vector2(-40, 0));
            osuChat.InsertLine(8298, "somunia", "lrc0-03", OsbOrigin.CentreRight, new Vector2(-21, 11));
            osuChat.InsertLine(11919, "somunia", "lrc0-04", OsbOrigin.CentreRight, new Vector2(-18, 0));
            osuChat.InsertLine(14160, "somunia", "lrc0-01", OsbOrigin.CentreRight, new Vector2(-26, 0));
            osuChat.InsertLine(16574, "somunia", "lrc0-05", OsbOrigin.CentreRight, new Vector2(-24, 0));
            osuChat.InsertLine(19333, "somunia", "lrc0-06", OsbOrigin.CentreRight, new Vector2(-43, 0));
            osuChat.InsertLine(20712, "somunia", "lrc0-07", OsbOrigin.CentreRight, new Vector2(-41, 0));
            osuChat.InsertLine(22091, "somunia", "lrc0-08", OsbOrigin.CentreRight, new Vector2(-35, 11));

            // nyankobrq - verse1
            osuChat.InsertLine(25195, "nyankobrq", "lrc1-01", OsbOrigin.CentreLeft, new Vector2(+26, 0));
            osuChat.InsertLine(27953, "nyankobrq", "lrc1-02", OsbOrigin.CentreLeft, new Vector2(+26, 0));
            osuChat.InsertLine(30712, "nyankobrq", "lrc1-03", OsbOrigin.CentreLeft, new Vector2(+26, 0));
            osuChat.InsertLine(33298, "nyankobrq", "lrc1-04", OsbOrigin.CentreLeft, new Vector2(+22, 0));
            osuChat.InsertLine(36747, "nyankobrq", "lrc1-05", OsbOrigin.CentreLeft, new Vector2(+60, 0));
            osuChat.InsertLine(38126, "nyankobrq", "lrc1-06", OsbOrigin.CentreLeft, new Vector2(+62, 0));
            osuChat.InsertLine(39333, "nyankobrq", "lrc1-07", OsbOrigin.CentreLeft, new Vector2(+34, 0));
            osuChat.InsertLine(41919, "nyankobrq", "lrc1-08", OsbOrigin.CentreLeft, new Vector2(+54, 11));
            osuChat.InsertLine(44850, "nyankobrq", "lrc1-09", OsbOrigin.CentreLeft, new Vector2(+42, 11));
            osuChat.InsertLine(48126, "nyankobrq", "lrc1-10", OsbOrigin.CentreLeft, new Vector2(+28, 0));
            osuChat.InsertLine(50022, "nyankobrq", "lrc1-11", OsbOrigin.CentreLeft, new Vector2(+36, 0));
            osuChat.InsertLine(52953, "nyankobrq", "lrc1-12", OsbOrigin.CentreLeft, new Vector2(+28, 0));
            osuChat.InsertLine(56402, "nyankobrq", "lrc1-13", OsbOrigin.CentreLeft, new Vector2(+48, 0));
            osuChat.InsertLine(59160, "nyankobrq", "lrc1-14", OsbOrigin.CentreLeft, new Vector2(+49, 0));
            osuChat.InsertLine(61402, "nyankobrq", "lrc1-15", OsbOrigin.CentreLeft, new Vector2(+36, 0));
            osuChat.InsertLine(64160, "nyankobrq", "lrc1-16", OsbOrigin.CentreLeft, new Vector2(-4, 0));
            osuChat.InsertLine(68126, "nyankobrq", "lrc1-17", OsbOrigin.CentreLeft, new Vector2(+44, 0));


            // somunia - hook
            osuChat.InsertLine(69333, "somunia", "lrc0-01", OsbOrigin.CentreRight, new Vector2(-26, 0));
            osuChat.InsertLine(71747, "somunia", "lrc0-02", OsbOrigin.CentreRight, new Vector2(-40, 0));
            osuChat.InsertLine(74505, "somunia", "lrc0-03", OsbOrigin.CentreRight, new Vector2(-21, 11));
            osuChat.InsertLine(78126, "somunia", "lrc0-04", OsbOrigin.CentreRight, new Vector2(-18, 0));
            osuChat.InsertLine(80367, "somunia", "lrc0-01", OsbOrigin.CentreRight, new Vector2(-26, 0));
            osuChat.InsertLine(82781, "somunia", "lrc0-05", OsbOrigin.CentreRight, new Vector2(-24, 0));
            osuChat.InsertLine(85540, "somunia", "lrc0-06", OsbOrigin.CentreRight, new Vector2(-43, 0));
            osuChat.InsertLine(86919, "somunia", "lrc0-07", OsbOrigin.CentreRight, new Vector2(-41, 0));
            osuChat.InsertLine(88298, "somunia", "lrc0-08", OsbOrigin.CentreRight, new Vector2(-35, 11));

            // yaca - verse2

            // somunia - verse 3

            // somunia - hook
            osuChat.InsertLine(179678, "somunia", "lrc0-01", OsbOrigin.CentreRight, new Vector2(-26, 0));
            osuChat.InsertLine(182091, "somunia", "lrc0-02", OsbOrigin.CentreRight, new Vector2(-40, 0));

            // nyankobrq - hook
            osuChat.InsertLine(184850, "nyankobrq", "lrc0-03L", OsbOrigin.CentreLeft, new Vector2(+26, 11));
            osuChat.InsertLine(188471, "nyankobrq", "lrc0-04", OsbOrigin.CentreLeft, new Vector2(+24, 0));

            // somunia - hook
            osuChat.InsertLine(190712, "somunia", "lrc0-01", OsbOrigin.CentreRight, new Vector2(-26, 0));
            osuChat.InsertLine(193126, "somunia", "lrc0-05", OsbOrigin.CentreRight, new Vector2(-24, 0));

            // yaca - hook
            osuChat.InsertLine(195884, "yaca", "lrc0-06", OsbOrigin.CentreLeft, new Vector2(+10, 0));
            osuChat.InsertLine(197264, "yaca", "lrc0-07", OsbOrigin.CentreLeft, new Vector2(+4, 0));
            osuChat.InsertLine(198643, "yaca", "lrc0-08L", OsbOrigin.CentreLeft, new Vector2(-2, 11));

            // somunia - hook
            osuChat.InsertLine(201747, "somunia", "lrc0-01", OsbOrigin.CentreRight, new Vector2(-26, 0));
            osuChat.InsertLine(204160, "somunia", "lrc0-02", OsbOrigin.CentreRight, new Vector2(-40, 0));
            osuChat.InsertLine(206919, "somunia", "lrc0-03", OsbOrigin.CentreRight, new Vector2(-21, 11));
            osuChat.InsertLine(210540, "somunia", "lrc0-04", OsbOrigin.CentreRight, new Vector2(-18, 0));

            List<Chat> chats = osuChat.GetVisibleChats();
            foreach (Chat chatt in chats)
            {
                chatt.Fade(213298, 216057, 0);
            }
        }


        public static string ConvertMsToTimeFormat(int milliseconds)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(milliseconds);
            return string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
        }

        class ChatController(StoryboardObjectGenerator ctx, StoryboardLayer layer, FontGenerator font, Vector2 spawnPosRight, Vector2 spawnPosLeft)
        {
            private readonly Vector2 spawnPosRight = spawnPosRight;
            private readonly Vector2 spawnPosLeft = spawnPosLeft;
            private readonly StoryboardObjectGenerator ctx = ctx;
            private readonly FontGenerator font = font;
            private readonly StoryboardLayer layer = layer;
            private readonly OsbEasing easing = OsbEasing.OutCirc;
            private readonly List<Chat> visibleChats = [];

            public void InsertLine(int time, string profileName, string lrcFileName, OsbOrigin origin, Vector2 profileOffset)
            {

                Vector2 spawnPos = origin == OsbOrigin.CentreLeft ? spawnPosLeft : spawnPosRight;
                string text;
                if (origin == OsbOrigin.CentreRight) { text = $"{ConvertMsToTimeFormat(time)} -- {profileName}"; } else { text = $"{profileName} -- {ConvertMsToTimeFormat(time)}"; }

                Chat insertedChat = new(ctx, text, $"sb/pfp/{profileName}.jpg", $"sb/lrc/{lrcFileName}.png", layer, font, origin, spawnPos, profileOffset);
                insertedChat.InitSprite(time);
                insertedChat.Fade(time, 0.5f, 1);

                visibleChats.Add(insertedChat);
                foreach (Chat chat in visibleChats)
                {
                    chat.MoveY(time, 0.5f, 60, easing);
                }

                RemoveOutsideRangeChat(time, 0);
            }

            private void RemoveOutsideRangeChat(int time, float outPosY)
            {
                for (int i = visibleChats.Count - 1; i > -1; i--)
                {
                    if (visibleChats[i].GetPosition().Y <= outPosY)
                    {
                        visibleChats[i].Fade(time, 0);
                        visibleChats.RemoveAt(i);
                    }
                }

                ctx.Log($"{ConvertMsToTimeFormat(time)}: Removing chats outside range. Visible chats count: {visibleChats.Count}");
                foreach (var chat in visibleChats)
                {
                    ctx.Log($"Visible chat filename: {chat.GetFileName()}, position: {chat.GetPosition()}");
                }
            }

            public List<Chat> GetVisibleChats()
            {
                return visibleChats;
            }
        }

        class Chat
        {
            private readonly StoryboardObjectGenerator ctx;
            private readonly OsbSprite pfp;
            private readonly OsbSprite messageImg;
            private readonly List<OsbSprite> profileS;
            private readonly List<OsbSprite> sprites = [];
            private readonly string FileName;
            private Vector2 Position;
            public Chat(StoryboardObjectGenerator ctx, string profileName, string profilePath, string messageImgPath, StoryboardLayer layer, FontGenerator font, OsbOrigin origin, Vector2 spawnPos, Vector2 profilePosOffset)
            {
                this.ctx = ctx;
                if (origin != OsbOrigin.CentreLeft && origin != OsbOrigin.CentreRight) throw new NotSupportedException("Only CentreLeft and CentreRight origins are supported.");

                // message
                FileName = messageImgPath;
                messageImg = layer.CreateSprite(messageImgPath, origin, spawnPos);

                // pfp
                Vector2 pfpPos = Vector2.Zero;
                if (origin == OsbOrigin.CentreRight) { pfpPos = new Vector2(spawnPos.X + 20, spawnPos.Y - 5); }
                else if (origin == OsbOrigin.CentreLeft) { pfpPos = new Vector2(spawnPos.X - 20, spawnPos.Y); }

                pfp = layer.CreateSprite(profilePath, OsbOrigin.Centre, pfpPos);

                var (lrcSize, offsets) = CalculateFontBaseWidth(font, profileName, origin);
                int d = origin == OsbOrigin.CentreLeft ? -1 : +1;

                // profile text
                Vector2 textOffset = new(this.ctx.GetMapsetBitmap(messageImg.TexturePath).Width, this.ctx.GetMapsetBitmap(messageImg.TexturePath).Height / 5);

                profileS = LyricSpritesFactoryHorizontal(layer, font, profileName, origin, new Vector2(spawnPos.X - (textOffset.X / (float)(Math.Floor((lrcSize.X + offsets.First()) / 10) - 1) * d), spawnPos.Y - textOffset.Y) + profilePosOffset);

                sprites = [pfp, messageImg, .. profileS];
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

            private void SetPosition(int time)
            {
                Position = pfp.PositionAt(time);
            }

            public Vector2 GetPosition()
            {
                return Position;
            }

            public string GetFileName()
            {
                return FileName;
            }

            public void InitSprite(int time)
            {
                messageImg.Scale(time, 480.0f / ctx.GetMapsetBitmap(messageImg.TexturePath).Height * 0.075);
                pfp.Scale(time, 854.0 / ctx.GetMapsetBitmap(pfp.TexturePath).Width * 0.04);
                SetPosition(time);

                float fontScale = 0.5f;
                foreach (OsbSprite sprite in profileS)
                {
                    sprite.Scale(time, fontScale);
                }

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

            public void MoveY(int time, float snapDuration, float relativePosY, OsbEasing easing)
            {
                int duration = (int)(snapDuration * ctx.Beatmap.TimingPoints.First().BeatDuration);
                foreach (OsbSprite sprite in sprites)
                {
                    sprite.MoveY(easing, time - duration, time, sprite.PositionAt(time - duration).Y, sprite.PositionAt(time - duration).Y - relativePosY);
                }
                SetPosition(time);
            }
        }
    }
}
