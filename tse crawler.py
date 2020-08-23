import requests

def search(name):
    res=requests.get('http://www.tsetmc.com/tsev2/data/search.aspx?skey='+name)
    return res.text
