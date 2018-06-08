SELECT country.Name FROM (
select countrylanguage.CountryCode, COUNT(CountryCode) as OffCount from countrylanguage
where countrylanguage.IsOfficial='T'
group by countrylanguage.CountryCode
having Count(IsOfficial)>=2
)AS officialCount,
(select countrylanguage.CountryCode, COUNT(CountryCode) as NonOffCount from countrylanguage
where countrylanguage.IsOfficial='F'
group by countrylanguage.CountryCode ) as nonOfficialCount, country 
INNER JOIN countrylanguage ON countrylanguage.CountryCode=country.Code
WHERE nonOfficialCount.NonOffCount>(officialCount.OffCount*2) AND countrylanguage.CountryCode=officialCount.CountryCode AND countrylanguage.CountryCode=nonOfficialCount.CountryCode
group by country.Code