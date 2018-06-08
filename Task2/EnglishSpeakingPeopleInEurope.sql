SELECT sum((country.Population*countrylanguage.Percentage)/100) AS englishSpeakingPeopleInEurope FROM  country
INNER JOIN countrylanguage ON countrylanguage.CountryCode=country.Code
WHERE countrylanguage.Language='English' AND country.Continent='Europe';