import requests

def search(name):
    res=requests.get('http://www.tsetmc.com/tsev2/data/search.aspx?skey='+name)
    return res.text

def reform_search_data(string):
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


def get_history_url(formeditem):
    return 'http://www.tsetmc.com/Loader.aspx?ParTree=151311&i='+formeditem[0]

def get_table_url(formeditem):
    return 'http://www.tsetmc.com/tsev2/data/instinfodata.aspx?i='+formeditem[0]+'&c=25+'