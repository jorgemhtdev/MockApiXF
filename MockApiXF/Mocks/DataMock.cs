namespace MockApiXF
{
    using MockApiXF.Models;
    using System;
    using System.Collections.Generic;

    public static class DataMock
    {
        public static IEnumerable<Comic> CargarComics()
        {
            return new List<Comic>
            {
                new Comic()
                {
                    Id = 1,
                    Isbn = "978-84-18475-51-1",
                    Nombre = "BATMAN: TRES JOKERS NÚM. 1 DE 3",
                    NombreOriginal = "Batman: Three Jokers Book One USA",
                    Desc ="Batman: Tres Jokers reexamina el mito de quién (o qué) es el Joker y qué subyace tras su eterna batalla contra el Caballero Oscuro. ¿Por qué existen tres Jokers? Tras años de especulaciones y grandes expectativas desde su aparición en el especial Universo DC: Renacimiento, ha llegado el momento de dar respuesta a tan inquietante pregunta. Responsable de la exitosa saga Liga de la Justicia: La guerra de Darkseid, el equipo creativo formado por Geoff Johns (El Reloj del Juicio Final, Batman: Tierra uno) y Jason Fabok (Batman/Flash: La chapa, La Cosa del Pantano: Santos con pies de barro) se reúne de nuevo para relatar la historia definitiva del Hombre Murciélago y el Príncipe Payaso del Crimen, a través de una miniserie de tres entregas.",
                    FechaDeVenta= DateTime.UtcNow,
                    Formato="Cartoné",
                    NumPag = 48,
                    Tamano = string.Empty,
                    Peso = 290,
                    Color = true,
                    Precio = 10.4,
                    Photo = "https://raw.githubusercontent.com/jorgemhtdev/comic-json/main/img/tres-jokers-01.jpg",
                    Novedad = true,
                    Agotado = false,
                    Disponibilidad = true,
                    Url = "https://www.ecccomics.com/comic/batman-tres-jokers-num-01-de-3-9110.aspx"
                },
                new Comic()
                {
                    Id = 2,
                    Isbn = "978-84-18475-51-1",
                    Nombre = "BATMAN: TRES JOKERS NÚM. 1 DE 3",
                    NombreOriginal = "Batman: Three Jokers Book One USA",
                    Desc ="Batman: Tres Jokers reexamina el mito de quién (o qué) es el Joker y qué subyace tras su eterna batalla contra el Caballero Oscuro. ¿Por qué existen tres Jokers? Tras años de especulaciones y grandes expectativas desde su aparición en el especial Universo DC: Renacimiento, ha llegado el momento de dar respuesta a tan inquietante pregunta. Responsable de la exitosa saga Liga de la Justicia: La guerra de Darkseid, el equipo creativo formado por Geoff Johns (El Reloj del Juicio Final, Batman: Tierra uno) y Jason Fabok (Batman/Flash: La chapa, La Cosa del Pantano: Santos con pies de barro) se reúne de nuevo para relatar la historia definitiva del Hombre Murciélago y el Príncipe Payaso del Crimen, a través de una miniserie de tres entregas.",
                    FechaDeVenta= DateTime.UtcNow,
                    Formato="Cartoné",
                    NumPag = 48,
                    Tamano = string.Empty,
                    Peso = 290,
                    Color = true,
                    Precio = 10.4,
                    Photo = "https://raw.githubusercontent.com/jorgemhtdev/comic-json/main/img/tres-jokers-02.jpg",
                    Novedad = true,
                    Agotado = false,
                    Disponibilidad = true,
                    Url = "https://www.ecccomics.com/comic/batman-tres-jokers-num-01-de-3-9110.aspx"
                },
                                new Comic()
                {
                    Id = 3,
                    Isbn = "978-84-18475-51-1",
                    Nombre = "BATMAN: TRES JOKERS NÚM. 1 DE 3",
                    NombreOriginal = "Batman: Three Jokers Book One USA",
                    Desc ="Batman: Tres Jokers reexamina el mito de quién (o qué) es el Joker y qué subyace tras su eterna batalla contra el Caballero Oscuro. ¿Por qué existen tres Jokers? Tras años de especulaciones y grandes expectativas desde su aparición en el especial Universo DC: Renacimiento, ha llegado el momento de dar respuesta a tan inquietante pregunta. Responsable de la exitosa saga Liga de la Justicia: La guerra de Darkseid, el equipo creativo formado por Geoff Johns (El Reloj del Juicio Final, Batman: Tierra uno) y Jason Fabok (Batman/Flash: La chapa, La Cosa del Pantano: Santos con pies de barro) se reúne de nuevo para relatar la historia definitiva del Hombre Murciélago y el Príncipe Payaso del Crimen, a través de una miniserie de tres entregas.",
                    FechaDeVenta= DateTime.UtcNow,
                    Formato="Cartoné",
                    NumPag = 48,
                    Tamano = string.Empty,
                    Peso = 290,
                    Color = true,
                    Precio = 10.4,
                    Photo = "https://raw.githubusercontent.com/jorgemhtdev/comic-json/main/img/tres-jokers-03.jpg",
                    Novedad = true,
                    Agotado = false,
                    Disponibilidad = true,
                    Url = "https://www.ecccomics.com/comic/batman-tres-jokers-num-01-de-3-9110.aspx"
                },
            };
        }
    }
}
