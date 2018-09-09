import googlemaps
from datetime import datetime

gmaps = googlemaps.Client(key='AIzaSyAsErYgXxC9-joZdGvRb48L-weoEk93IEM')


# Geo-coding an address

geocode_result = gmaps.geocode("input_address")

