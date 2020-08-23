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

def extract_table(table_url):
    res=requests.get(table_url).text
    table=[]
    lines=res.split(';')
    rows=lines[2][0:-1].split(',')    
    for j in range(len(rows)):
        row=rows[j].split('@')
        for i in range(len(row)-1,-1,-1):
            table.append(row[i])
    return table