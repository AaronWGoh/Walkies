import googlemaps
from datetime import datetime

gmaps = googlemaps.Client(key='')

#Geocoding an address

geocode_result = gmaps.geocode("input_address")

