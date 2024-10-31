#zad 1
def parni(lista):
    dic = {
        'Parni': [],
        'Neparni': []
    }

    for el in lista:
        if el % 2 == 0:
            dic['Parni'].append(el)
        else:
            dic['Neparni'].append(el)
    return dic


print(parni([1, 7, 2, 4, 5]) )

#zad 2

def numlista(lista):
    dic = {}
    for x in lista:
        typeX = type(x).__name__

        if typeX not in dic:
            dic[typeX] = []

        dic[typeX].append(x)

    return dic

print(numlista((["Prvi", "Drugi", 2, 4, [3, 5]])))


#zad 3

def uredi(lista, n, m):
    listaNova = []
    i = 0
    for x in lista:
        if i < n:
            listaNova.append(x + m)
        else:
            listaNova.append(x - m)

        i+=1

    return listaNova

print( uredi([1, 2, 3, 4, 5], 3, 1))

#druga varijanta zad 3
def uredi2(lista, n, m):
    for i in range(min(n, len(lista))):
        lista[i] += m
    else:
        lista[i] -= m

    return lista

print( uredi([1, 2, 3, 4, 5], 3, 1))


#zad 4
def zbir(lista):
    novalista = []
    for i in range(len(lista) - 1):
        novalista.append(lista[i] + lista[i] + 1)
    return novalista

print( zbir([1, 2, 3, 4, 5]))

#moglo je i samo ovako
def zbir2(lista):
    return [lista[i] + lista[i + 1] for i in range(len(lista) - 1)]

# Primer korišćenja:
print(zbir2([1, 2, 3, 4, 5]))

#zad 5
def brojEl(lista):
    for i in range(len(lista)):
        if type(lista[i]).__name__ != 'list':
            lista[i] = -1
        else:
            lista[i] = len(lista[i])

    return lista

print(brojEl([[1, 2], [3, 4, 5], 'el', ['1', 1]]))

#zad 6
def razlika(lista1, lista2):
    rez = []
    for x in lista1:
        if x not in lista2:
            rez.append(x)
    return rez

print(razlika([1, 4, 6, "2", "6"], [4, 5, "2"]))

#chatgpt
def razlika2(lista1, lista2):
    return [x for x in lista1 if x not in lista2]

print(razlika2([1, 4, 6, "2", "6"], [4, 5, "2"]))

#zad 7
def saberi(lista):
    i = 0
    for x in lista:
        pom = 0
        for y in x:
            pom += y

        lista[i] = pom
        i+=1

    return lista

print(saberi([(1, 4, 6), (2, 4), (4, 1)]))

#drugi nacin
def saberi2(lista):
    return [sum(tupl) for tupl in lista]

print(saberi2([(1, 4, 6), (2, 4), (4, 1)]))

#zad 8
def izmeni(lista):
    listaCpy = lista.copy()
    suma = 0
    for i in range(len(lista)):
        suma += listaCpy[i]
        lista[i] = suma
    return lista

print(izmeni([1, 2, 4, 7, 9]))

#zad 9
def prosek(lista):
    return [sum(x)/len(x) for x in lista]

print(prosek([[1, 4, 6, 2], [4, 6, 2, 7], [3, 5], [5, 6, 2, 7]]))

#zad 10
def izbroj(lista):
    sum = 0
    for x in lista:
        if isinstance(x, list) :
            sum += izbroj(x)
        else:
            sum += 1

    return sum

print(izbroj([1, [1, 3, [2, 4, 5, [5, 5], 4]], [2, 4], 4, 6]))

#zad 11
def razlika3(list):
    listNew = []
    for i in range(len(list)-1):
        listNew.append(list[i] - list[i+1])

    return listNew

print(razlika3([8, 5, 3, 1, 1]))

#druga varijanta 11
def razlika4(list):
    return [list[i] - list[i+1] for i in range(len(list) - 1)]

print(razlika4([8, 5, 3, 1, 1]))

#zad 12
def presek3(lista1, lista2):
    return [ x for x in lista1 if x in lista2]
print(presek3([1, 4, "1", "8", 3, 7], [1, 9, "1"]))

#zad 13
def izmenii(lista):
    dic = {
        "np": [],
        "pp": []
    }

    for i in range(len(lista)):
        if i % 2 == 0:
            dic["pp"].append(lista[i] + 1)
        else:
            dic["np"].append(lista[i] - 1)

    return dic

print(izmenii([8, 6, 3, 1, 1]))

#zad 14
def unija(list1, list2):
    list = list1 + list2
    rez = []
    for x in list:
        if x not in rez:
            rez.append(x)
    return rez

print(unija([5, 4, "1", "8", 7], [1, 9, "1"]))

#zad 15
def izdvoji(lista):
    newList = []
    for i in range(len(lista)):
        k = i
        if k in range(len(lista[i])):
            newList.append(lista[i][k])
        else:
            newList.append(0)
    return newList

print(izdvoji([[5, 4, 4], [1, 9, 1], [5, 6], [4, 6, 10, 12]]))

#zad 16
def brojanje(dictionary):
    tipovi = {}
    for x in dictionary.values():
        tip = type(x).__name__
        if tip not in tipovi:
            tipovi[tip] = 0
        tipovi[tip] += 1
    return list(tipovi.items())

print(brojanje({1 : 4, 2 : [2, 3], 3 : [5, 6], 4 : 'test', 5 : 9, 6 : 8}))

#zad 17
def kreiraj(n):
    rezultat = []
    for i in range(n + 1):
        rezultat.append((i, sum(range(i + 1)) ** 2))
    return rezultat

print(kreiraj(4))

#zad 18
def kreiraj1(lista):
    rez = []
    for i in range(len(lista) - 1):
        razlika = [ x for x in lista[i] if x not in lista[i + 1]]
        rez.append(razlika)
    return rez

print(kreiraj1([[1, 2, 3], [2, 4, 5], [4, 5, 6, 7], [1, 5]]))

#zad 19
def stepenuj(lista):
    rez = []
    for element in lista:
        if len(element) > 1:
            stepen = element[0]
            for i in range(1, len(element)):
                stepen = stepen ** element[i]
            rez.append(stepen)
        else:
            rez.append(element[0])
    return rez

print(stepenuj([(1, 4, 2), (2, 5, 1), (2, 2, 2, 2), (5,)]))

#zad 20
def boje(string): #uzima se prve dve cifre za RED, druge dve za GREEN i onda zadnje dve za BLUE
    red = int(string[1:3], 16)
    green = int(string[3:5], 16)
    blue = int(string[5:7], 16)

    return {"Red": red, "Green": green, "Blue": blue}

print(boje("#FA1AA0"))