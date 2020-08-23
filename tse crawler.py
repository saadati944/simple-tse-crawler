import requests

def search(name):
    res=requests.get('http://www.tsetmc.com/tsev2/data/search.aspx?skey='+name)
    return res.text

def reform_data(string):
    '''
    the reformed data has three elements:
    0:id
    1:name
    2:description
    '''
    reformed=[]
    lines=string.split(';')
    for line in lines :
        try:
            data=line.split(',')
            reformed.append([data[2],data[0],data[1]])
        except:
            pass
    return reformed