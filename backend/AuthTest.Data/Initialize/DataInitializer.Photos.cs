using AuthTest.Data.Entities;
using AuthTest.Data.Extensions;
using AuthTest.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Initialize
{
    public static partial class DataInitializer
    {
        public static void SeedPhotos(ApplicationDbContext context)
        {
            context.Photos.AddOrUpdate(new Photo()
            {
                Id = 1,
                Url = "https://live.staticflickr.com/4851/46063388491_5d2202d87b_k.jpg",
                Description = "Монах и кот",
                CityId = (int)Enums.CityEnum.Zvenigorod,
                CreatedDt = new DateTime(2018, 11, 26)
            });
            context.Photos.AddOrUpdate(new Photo()
            {
                Id = 2,
                Url = "https://live.staticflickr.com/4907/32192066068_55f935525c_k.jpg",
                Description = "Рождество-Богородицкий собор - главный храм Саввино-Сторожевского монастыря",
                CityId = (int)Enums.CityEnum.Zvenigorod,
                CreatedDt = new DateTime(2018, 11, 26)
            });
            context.Photos.AddOrUpdate(new Photo()
            {
                Id = 3,
                Url = "https://live.staticflickr.com/7805/47230320242_5884823e34_k.jpg",
                Description = "Осенний рассвет в районе Аэропорт",
                CityId = (int)Enums.CityEnum.Moscow,
                CreatedDt = new DateTime(2018, 11, 6)
            });

            context.Photos.AddOrUpdate(new Photo()
            {
                Id = 4,
                Url = "https://live.staticflickr.com/4865/43914958270_8b1c07b320_k.jpg",
                Description = "Яуза у Бауманского Университета",
                CityId = (int)Enums.CityEnum.Moscow,
                CreatedDt = new DateTime(2018, 10, 28)
            });
            context.Photos.AddOrUpdate(new Photo()
            {
                Id = 5,
                Url = "https://live.staticflickr.com/1925/45682173192_f56b129555_k.jpg",
                Description = "Осенний вид из Сибинтека на Ваганьковское кладбище и Сити",
                CityId = (int)Enums.CityEnum.Moscow,
                CreatedDt = new DateTime(2018, 10, 8)
            });


            context.Photos.AddOrUpdate(new Photo()
            {
                Id =6,
                Url = "https://live.staticflickr.com/1843/43876910545_2504c9674f_k.jpg",
                Description = "Алматинская телебашня и горы",
                CityId = (int)Enums.CityEnum.Almaty,
                CreatedDt = new DateTime(2018, 9, 16)
            });

            context.Photos.AddOrUpdate(new Photo()
            {
                Id = 7,
                Url = "https://live.staticflickr.com/5619/21725414352_03c556bedb_k.jpg",
                Description = "Форосская церковь",
                CityId = (int)Enums.CityEnum.Crimea,
                CreatedDt = new DateTime(2013, 8, 20)
            });

            context.SaveChanges();


            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 1, TagId = (int)TagEnum.Cats });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 1, TagId = (int)TagEnum.People });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 1, TagId = (int)TagEnum.Snow });

           
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 2, TagId = (int)TagEnum.Arcitechture });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 2, TagId = (int)TagEnum.Century15 });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 2, TagId = (int)TagEnum.Church });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 2, TagId = (int)TagEnum.Monastery });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 2, TagId = (int)TagEnum.RussianStyle });

           
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 3, TagId = (int)TagEnum.Sunrise });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 3, TagId = (int)TagEnum.Sky });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 3, TagId = (int)TagEnum.FromWindow });

            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 4, TagId = (int)TagEnum.River });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 4, TagId = (int)TagEnum.Sky });

            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 5, TagId = (int)TagEnum.FromWindow });

            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 6, TagId = (int)TagEnum.Mountains });

            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 7, TagId = (int)TagEnum.Sea });
            context.PhotoTags.AddOrUpdate(new PhotoTag { PhotoId = 7, TagId = (int)TagEnum.Century19 });
        }
    }
}
