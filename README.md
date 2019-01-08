# SimpleHtmlScraper

This is a simple html scraper specifically made for tripadvisor. I was looking for a part-time job at the time and I wanted to automate the process of collecting employer email addresses.

# Requirement

In order to be able to build this project, The HTML Agility pack is required. This can be retrieved using the NuGet package manager in visual studio. The link at the start can be changed to match another location.

# Specs

It makes use of the HTML Agility pack to retrieve documents and scrape href tags and their values from the specified document. A regular expression is then used to retrieve an email address from the page. Due to this, the results are not always 100% accurate.
