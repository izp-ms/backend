using Domain.Entities;

namespace Infrastructure.Data.Seeder;

public class PostcardSeeder
{
    public static IEnumerable<Postcard> GetPostcardSeeder()
    {
        IEnumerable<Postcard> postcards = new List<Postcard>()
        {
            new Postcard()// ID: 1
            {
                Title = "Miłego dnia bracie",
                Content = "Życze cie miłego wypoczynku i smacznej kawusi.",
                PostcardDataId = 7,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 7, 23),
            },
            new Postcard()// ID: 2
            {
                Title = "Dzieki za kawusie",
                Content = "Także przesyłam pozdrowienia.",
                PostcardDataId = 15,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 5, 15),
            },
            new Postcard()// ID: 3
            {
                Title = "Odwiedziny w katowicach",
                Content = "Małą pamiątka po wizycie w Gliwicach. Dzięki za zaproszenie.",
                PostcardDataId = 3,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 1, 12)
            },
            new Postcard()// ID: 4
            {
                Title = "Wypoczynek nad morzem",
                Content = "Pozdrawiamy z nad morza, szkoda że Cię z nami tu nie ma.",
                PostcardDataId = 12,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 6, 4)
            },
            new Postcard()// ID: 5
            {
                Title = "Wakazje na Helu",
                Content = "Woda zimna, ale chociaż rybka była dobra.",
                PostcardDataId = 18,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 3, 20)
            },
            new Postcard()// ID: 6
            {
                Title = "Wawel",
                Content = "Piękne miejsce <3",
                PostcardDataId = 5,
                Type = "PLACE",
                CreatedAt = new DateTime(2023, 10, 7)
            },
            new Postcard()// ID: 7
            {
                Title = "Wycieczka do Stolicy",
                Content = "Cudowny wyjazd, cudowni ludzie.",
                PostcardDataId = 10,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 6, 12)
            },
            new Postcard()// ID: 8
            {
                Title = "Historia obok mnie",
                Content = "Wizyta w muzeum dała mi wiele do myślenia.",
                PostcardDataId = 2,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 7, 10)
            },
            new Postcard()// ID: 9
            {
                Title = "Rocznica",
                Content = "Cudowny pomysł na udaną randkę.",
                PostcardDataId = 19,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 4, 21)
            },
            new Postcard()// ID: 10
            {
                Title = "Rodzinny weekend",
                Content = "Dzieci zadowolone, żona spokojna, a ja razem z nimi",
                PostcardDataId = 8,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 8, 22)
            },
            new Postcard()// ID: 11
            {
                Title = "Zielono kolorowo",
                Content = "Nigdy w życiu nie widziałem naraz tak dużo różnych rodzajów roślin",
                PostcardDataId = 1,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 1, 29)
            },
            new Postcard()// ID: 12
            {
                Title = "Przyjacielski wypad",
                Content = "Któżby pomyślał że można się tak świetnie bawić w ZOO",
                PostcardDataId = 11,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 6, 11)
            },
            new Postcard()// ID: 13
            {
                Title = "Przyroda wokoło",
                Content = "Piękne miejsce na spędzenie miejsca musimy kiedys tam wyskoczyć razem na pogaduchy i wspólny piknik.",
                PostcardDataId = 6,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 12, 17)
            },
            new Postcard()// ID: 14
            {
                Title = "Morski wypoczynek",
                Content = "Życzymy miłego wypoczynku z nam morza.",
                PostcardDataId = 14,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 2, 1),
            },
            new Postcard()// ID: 15
            {
                Title = "Spacer",
                Content = "Przyroda to lustro naszej społeczności jeśli ona wyginie to my wyginiemy z nią.",
                PostcardDataId = 9,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 7, 12),
            },
            new Postcard()// ID: 16
            {
                Title = "IEM",
                Content = "Razem z przyjaciółmi na wydarzeniu ;)",
                PostcardDataId = 4,
                Type = "EVENT",
                CreatedAt = new DateTime(2019, 4, 15)
            },
            new Postcard()// ID: 17
            {
                Title = "Me with the boys",
                Content = "Wspinaczna i zakwasy cudwona sprawa",
                PostcardDataId = 17,
                Type = "PLACE",
                CreatedAt = new DateTime(2018, 10, 20)
            },
            new Postcard()// ID: 18
            {
                Title = "Spotkanie z historią",
                Content = "Ciekawe doświadczenie dotknąć tych murów",
                PostcardDataId = 13,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 7, 8)
            },
            new Postcard()// ID: 19
            {
                Title = "Vintage experience",
                Content = "Uwielbiam ten styl, chciałabym mieć tak w domu",
                PostcardDataId = 16,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 6, 12)
            },
            new Postcard()// ID: 20
            {
                Title = "Pozdrowienia z gór",
                Content = "Górale, oscypki i piękne góry",
                PostcardDataId = 20,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 2, 5)
            },
            new Postcard()// ID: 21
            {
                Title = "Wyjście zapoznawcze",
                Content = "Piekny park w raz z piękną palmiarnią, na pewno wróce",
                PostcardDataId = 1,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 11, 30)
            },
            new Postcard()// ID: 22
            {
                Title = "Spektakl",
                Content = "Miłe wspomnienie :)",
                PostcardDataId = 12,
                Type = "PLACE",
                CreatedAt = new DateTime(2018, 9, 25)
            },
            new Postcard()// ID: 23
            {
                Title = "Niebo gwieździste",
                Content = "Gwiazdy są takie same nie ważne gdzie jesteś na ziemi",
                PostcardDataId = 8,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 8, 14)
            },
            new Postcard()// ID: 24
            {
                Title = "WOOOOW",
                Content = "Jazda na motorze, ekstremalny sport to jest to!!!",
                PostcardDataId = 7,
                Type = "PLACE",
                CreatedAt = new DateTime(2023, 1, 7)
            },
            new Postcard()// ID: 25
            {
                Title = "Stand-Up",
                Content = "XDDDDDDDD",
                PostcardDataId = 19,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 5, 22)
            },
            new Postcard()// ID: 26
            {
                Title = "Królewski Pałac",
                Content = "Zostawiłem mały cache pod kamieniem na wschód od północnego dziedzińca kto pierwszy ten lepszy ;)",
                PostcardDataId = 5,
                Type = "PLACE",
                CreatedAt = new DateTime(2021, 3, 19)
            },
            new Postcard()// ID: 27
            {
                Title = "Historia w zasięgu ręki",
                Content = "Blisko mojego domu, fajnie się dowiedzieć coś o swojej hitorii.",
                PostcardDataId = 3,
                Type = "PLACE",
                CreatedAt = new DateTime(2019, 12, 10)
            },
            new Postcard()// ID: 28
            {
                Title = "Dzieło sztuki",
                Content = "To jest to co w swoim życiu warto zobaczyć.",
                PostcardDataId = 15,
                Type = "PLACE",
                CreatedAt = new DateTime(2022, 6, 28)
            },
            new Postcard()// ID: 29
            {
                Title = "Morze, może, morze",
                Content = "Jak ja lubie morze, może.",
                PostcardDataId = 18,
                Type = "PLACE",
                CreatedAt = new DateTime(2018, 7, 3)
            },
            new Postcard()// ID: 30
            {
                Title = "Kawałek gliwickiej historii",
                Content = "Piękna majestatyczna radiostacja tylko z drewna, warta obejrzenia.",
                PostcardDataId = 2,
                Type = "PLACE",
                CreatedAt = new DateTime(2020, 4, 11)
            },


        };
        return postcards;
    }
}
