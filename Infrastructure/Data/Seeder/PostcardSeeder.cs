﻿using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class PostcardSeeder
{
    public static IEnumerable<Postcard> GetPostcardSeeder(DataContext dataContext)
    {
        List<PostcardData> postcard = dataContext.PostcardData.ToList();
        IEnumerable<Postcard> postcards = new List<Postcard>()
        {
            new Postcard()
            {
                Title = "Miłego dnia bracie",
                Content = "Życze cie miłego wypoczynku i smacznej kawusi.",
                PostcardDataId = postcard[6].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 7, 23),
            },
            new Postcard()
            {
                Title = "Dzieki za kawusie",
                Content = "Także przesyłam pozdrowienia.",
                PostcardDataId = postcard[14].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 5, 15),
            },
            new Postcard()
            {
                Title = "Odwiedziny w katowicach",
                Content = "Małą pamiątka po wizycie w Katowicach. Dzięki za zaproszenie.",
                PostcardDataId = postcard[2].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 1, 12)
            },
            new Postcard()
            {
                Title = "Wypoczynek nad morzem",
                Content = "Pozdrawiamy z nad morza, szkoda że Cię z nami tu nie ma.",
                PostcardDataId = postcard[11].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 6, 4)
            },
            new Postcard()
            {
                Title = "Wakazje na Helu",
                Content = "Woda zimna, ale chociaż rybka była dobra.",
                PostcardDataId = postcard[17].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 3, 20)
            },
            new Postcard()
            {
                Title = "Wawel",
                Content = "Piękne miejsce <3",
                PostcardDataId = postcard[4].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2023, 10, 7)
            },
            new Postcard()
            {
                Title = "Wycieczka do Stolicy",
                Content = "Cudowny wyjazd, cudowni ludzie.",
                PostcardDataId = postcard[9].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 6, 12)
            },
            new Postcard()
            {
                Title = "Historia obok mnie",
                Content = "Wizyta w muzeum dała mi wiele do myślenia.",
                PostcardDataId = postcard[1].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 7, 10)
            },
            new Postcard()
            {
                Title = "Rocznica",
                Content = "Cudowny pomysł na udaną randkę.",
                PostcardDataId = postcard[18].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 4, 21)
            },
            new Postcard()
            {
                Title = "Rodzinny weekend",
                Content = "Dzieci zadowolone, żona spokojna, a ja razem z nimi",
                PostcardDataId = postcard[7].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 8, 22)
            },
            new Postcard()
            {
                Title = "Zielono kolorowo",
                Content = "Nigdy w życiu nie widziałem naraz tak dużo różnych rodzajów roślin",
                PostcardDataId = postcard[0].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 1, 29)
            },
            new Postcard()
            {
                Title = "Przyjacielski wypad",
                Content = "Któżby pomyślał że można się tak świetnie bawić w ZOO",
                PostcardDataId = postcard[10].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 6, 11)
            },
            new Postcard()
            {
                Title = "Przyroda wokoło",
                Content = "Piękne miejsce na spędzenie miejsca musimy kiedys tam wyskoczyć razem na pogaduchy i wspólny piknik.",
                PostcardDataId = postcard[5].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 12, 17)
            },
            new Postcard()
            {
                Title = "Morski wypoczynek",
                Content = "Życzymy miłego wypoczynku z nam morza.",
                PostcardDataId = postcard[13].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 2, 1),
            },
            new Postcard()
            {
                Title = "Spacer",
                Content = "Przyroda to lustro naszej społeczności jeśli ona wyginie to my wyginiemy z nią.",
                PostcardDataId = postcard[8].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 7, 12),
            },
            new Postcard()
            {
                Title = "IEM",
                Content = "Razem z przyjaciółmi na wydarzeniu ;)",
                PostcardDataId = postcard[3].Id,
                Type = "EVENT",
                CreatedAt = new DateTime(2019, 4, 15)
            },
            new Postcard()
            {
                Title = "Me with the boys",
                Content = "Wspinaczna i zakwasy cudwona sprawa",
                PostcardDataId = postcard[16].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2018, 10, 20)
            },
            new Postcard()
            {
                Title = "Spotkanie z historią",
                Content = "Ciekawe doświadczenie dotknąć tych murów",
                PostcardDataId = postcard[12].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 7, 8)
            },
            new Postcard()
            {
                Title = "Vintage experience",
                Content = "Uwielbiam ten styl, chciałabym mieć tak w domu",
                PostcardDataId = postcard[15].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 6, 12)
            },
            new Postcard()
            {
                Title = "Pozdrowienia z gór",
                Content = "Górale, oscypki i piękne góry",
                PostcardDataId = postcard[19].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 2, 5)
            },
            new Postcard()
            {
                Title = "Wyjście zapoznawcze",
                Content = "Piekny park w raz z piękną palmiarnią, na pewno wróce",
                PostcardDataId = postcard[0].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 11, 30)
            },
            new Postcard()
            {
                Title = "Spektakl",
                Content = "Miłe wspomnienie :)",
                PostcardDataId = postcard[11].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2018, 9, 25)
            },
            new Postcard()
            {
                Title = "Niebo gwieździste",
                Content = "Gwiazdy są takie same nie ważne gdzie jesteś na ziemi",
                PostcardDataId = postcard[7].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 8, 14)
            },
            new Postcard()
            {
                Title = "WOOOOW",
                Content = "Jazda na motorze, ekstremalny sport to jest to!!!",
                PostcardDataId = postcard[6].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2023, 1, 7)
            },
            new Postcard()
            {
                Title = "Stand-Up",
                Content = "XDDDDDDDD",
                PostcardDataId = postcard[18].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 5, 22)
            },
            new Postcard()
            {
                Title = "Królewski Pałac",
                Content = "Zostawiłem mały cache pod kamieniem na wschód od północnego dziedzińca kto pierwszy ten lepszy ;)",
                PostcardDataId = postcard[4].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 3, 19)
            },
            new Postcard()
            {
                Title = "Historia w zasięgu ręki",
                Content = "Blisko mojego domu, fajnie się dowiedzieć coś o swojej hitorii.",
                PostcardDataId = postcard[2].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 12, 10)
            },
            new Postcard()
            {
                Title = "Dzieło sztuki",
                Content = "To jest to co w swoim życiu warto zobaczyć.",
                PostcardDataId = postcard[14].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 6, 28)
            },
            new Postcard()
            {
                Title = "Morze, może, morze",
                Content = "Jak ja lubie morze, może.",
                PostcardDataId = postcard[17].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2018, 7, 3)
            },
            new Postcard()
            {
                Title = "Kawałek gliwickiej historii",
                Content = "Piękna majestatyczna radiostacja tylko z drewna, warta obejrzenia.",
                PostcardDataId = postcard[1].Id,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 4, 11)
            },


        };
        return postcards;
    }
}
