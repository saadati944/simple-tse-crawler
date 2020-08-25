import requests
import json

def search(name):
    res=requests.get('http://www.tsetmc.com/tsev2/data/search.aspx?skey='+name)
    return res.text
def fixlen(a,l):
        if len(a)<l:
            a+=' '*(l-len(a))
        elif len(a)>l:
            a=a[:-len(a)+l]
        return a
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

def get_printable_history(history_url):
    a=requests.get(history_url).text.split('\n')
    history=''
    for ln in a:
        if 'var TradeHistory=' in ln :
            history=ln
            break
    if len(history)<15:
        return 'coudn`t find trading history'
    history=history[history.find('[[')+2:-3].split('],[')
    out =' ______________________________________________________________________________ \n'
    out+='/                                 trading history                              \\\n'
    out+='|'+fixlen('date',9)+'|'+fixlen('final',11)+'|'+fixlen('min',11)+'|'+fixlen('max',11)+'|'+fixlen('count',11)+'|'+fixlen('volume',7)+'|'+fixlen('cost',12)+'|\n'
    for row in history:
        data=row[1:-1].split("','")
        out+='|'+fixlen(str(data[0]),9)+'|'+fixlen(str(data[1]),11)+'|'+fixlen(data[2],11)+'|'+fixlen(data[3],11)+'|'+fixlen(data[4],11)+'|'+fixlen(data[5],7)+'|'+fixlen(data[6],12)+'|\n'
    return out
def get_printable_table(table):
    out =' ___________________________________________________________________________\n'
    out+='/             sell                    |              buy                    \\\n'
    out+='|count       -volume      -cost       |cost        -volume      -count      |\n'
    for i in range(len(table)):
        if i%6==0 and i!=0:
           out+='|\n|'
        elif i%3==0:
            out+='|'
        else :
            out+='  '
        out+= fixlen(table[i],11)
    return out+'|'

print('tse crawler ver 1.0')

while True:
    try:
        name=input('enter name : ')
        searchres=search(name)
        reformed=reform_search_data(searchres)[0]
        print(reformed[1],'-',reformed[2])
        print()
        print(get_printable_table(extract_table(get_table_url(reformed))))
        print()
        print(get_printable_history(get_history_url(reformed)))
    except:
        print('Error ...')


print('\nend')
