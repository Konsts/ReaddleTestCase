SELECT country.Name FROM country 
INNER JOIN city ON city.ID=country.Capital
ORDER BY city.Population DESC 
LIMIT 0,5