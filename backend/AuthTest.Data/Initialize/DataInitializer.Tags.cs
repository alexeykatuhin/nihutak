using AuthTest.Data.Entities;
using AuthTest.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Initialize
{
    public static partial class DataInitializer
    {
        public static void SeedTags(ApplicationDbContext context)
        {
            context.Tags.AddOrUpdate(new Tag() { Id = (int)Enums.TagEnum.People, Name = "Люди" }
            , new Tag() { Id =(int)Enums.TagEnum.Cats, Name = "Коты" }
            , new Tag() { Id =(int)Enums.TagEnum.Snow, Name = "Снег" }
            , new Tag() { Id =(int)Enums.TagEnum.Arcitechture, Name = "Архитектура" }
            , new Tag() { Id =(int)Enums.TagEnum.RussianStyle, Name = "Русский стиль" }
            , new Tag() { Id =(int)Enums.TagEnum.Monastery, Name = "Монастырь" }
            , new Tag() { Id =(int)Enums.TagEnum.Century15, Name = "15 век" }
            , new Tag() { Id =(int)Enums.TagEnum.Church, Name = "Храм" }
            , new Tag() { Id =(int)Enums.TagEnum.Sky, Name = "Небо" }
            , new Tag() { Id =(int)Enums.TagEnum.Sunrise, Name = "Рассвет" }
            , new Tag() { Id =(int)Enums.TagEnum.FromWindow, Name = "Из окна" }
            , new Tag() { Id =(int)Enums.TagEnum.River, Name = "Река" }
            , new Tag() { Id =(int)Enums.TagEnum.ModernArcitechture, Name = "Современная архитектура" }
            , new Tag() { Id = (int)Enums.TagEnum.Mountains, Name = "Горы" }
            , new Tag() { Id = (int)Enums.TagEnum.Sea, Name = "Море" }
            , new Tag() { Id = (int)Enums.TagEnum.Century19, Name = "19 век" }

            );

        }
    }
}
