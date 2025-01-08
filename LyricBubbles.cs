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
        public override void Generate()
        {
            StoryboardLayer layer = GetLayer("LRC Layer");
            OsbSprite lrc00 = layer.CreateSprite("sb/lrc/lrc0-01.png", OsbOrigin.CentreRight, new Vector2(485, 200));
            lrc00.Fade(2953, 3126, 0, 1);
            lrc00.Fade(AudioDuration, 0);
            lrc00.MoveY(OsbEasing.InCubic, 2781, 3126, 225, 200);
            lrc00.MoveY(OsbEasing.InCubic, 5195, 5540, 200, 175);
            lrc00.Scale(0, 480 / GetMapsetBitmap(lrc00.TexturePath).Height * 0.075);

            OsbSprite pfp1 = layer.CreateSprite("sb/pfp/somunia.jpg", OsbOrigin.Centre, new Vector2(503, 180));
            pfp1.Fade(2953, 3126, 0, 1);
            pfp1.Fade(AudioDuration, 0);
            pfp1.MoveY(OsbEasing.InCubic, 2781, 3126, 205, 180);
            pfp1.MoveY(OsbEasing.InCubic, 5195, 5540, 180, 155);
            pfp1.Scale(0, 854.0 / GetMapsetBitmap(pfp1.TexturePath).Width * 0.04);

        }

        class Chat
        {
            private readonly OsbOrigin origin;
            private readonly Vector2 spawnPos;
            private readonly StoryboardLayer layer;
            private readonly OsbSprite pfp;
            private readonly OsbSprite messageImg;
            private readonly OsbSprite profile;
            public Chat(string profileName, string profilePath, string messageImgPath, StoryboardLayer layer, OsbOrigin origin, Vector2 spawnPos)
            {
                this.origin = origin;
                this.spawnPos = spawnPos;
                this.layer = layer;




            }
        }
    }
}
