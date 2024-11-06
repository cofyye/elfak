# ZAD1
from functools import reduce
from itertools import zip_longest

def poredak(lista1,lista2):

    return [
        (a, b, 'Jeste' if b == 2 * a else 'Nije') 
        for a, b in zip_longest(lista1, lista2, fillvalue=0)
    ]

print("Zad 1: " + str(poredak ([1, 7, 2, 4], [2, 5, 2]) ))

# ZAD 2

def spojidict(lista1,lista2):
    return[{'prvi': a, 'drugi': b} for a,b in zip_longest(lista1,lista2,fillvalue='-')]

print("Zad 2: " + str(spojidict([1, 7, 2, 4], [2, 5, 2])))

# ZAD 3

def spoji(lista1, lista2):

    return [(min(a, b), max(a, b), a + b) for a, b in zip_longest(lista1, lista2, fillvalue=0)]

print("Zad 3: " + str(spoji([1, 7, 2, 4], [2, 5, 2])))

# ZAD 4

def suma(lista):

    return reduce(lambda x, y: x + y, (suma(x) if isinstance(x, list) else x for x in lista), 0)

print("Zad 4: " + str(suma([[1, 2, 3], [4, 5, 6], [7, 8, 9]])))

# ZAD 5

def proizvod(A, B):
    return [
        sum(a * B[i] for a in sublist)
        for i, sublist in enumerate(A)
    ]

print("Zad 5: " + str(proizvod([[1, 2, 3], [4, 5, 6], [7, 8, 9]], [1, 2, 3])))

# ZAD 6

def objedini(lista1, lista2):
    return [
        (min(a, b), max(a, b)) 
        for a, b in zip_longest(lista1, lista2, fillvalue=0)
    ]

print("Zad 6: " + str(objedini([1, 7, 2, 4, 5], [2, 5, 2])))

# ZAD 7

def objedini(lista):
    return {t[0] : (list(t[1:]) if len(t) > 1 else None) for t in lista}

print("Zad 7: " + str(objedini([(1,), (3, 4, 5), (7,), (1, 4, 5), (6, 2, 1, 3)])))

# ZAD 8

def izracunaj(lista):
    return [
        reduce(lambda x, y: x * y, podlista) if isinstance(podlista, list) else podlista
        for podlista in lista
    ]

print("Zad 8: " + str(izracunaj([1, 5, [1, 5, 3], [4, 2], 2, [6, 3]])))

# ZAD 9

def zamena(lista, x):
    return [
        sum(lista[i+1:]) if lista[i] < x else lista[i]
        for i in range(len(lista))
    ]

print("Zad 9: " + str(zamena([1, 7, 5, 4, 9, 1, 2, 7], 5)))

# 2 NACIN

from functools import reduce

def zamena2(lista, x):
    return [
        reduce(lambda acc, val: acc + val, lista[i+1:], 0) if lista[i] < x else lista[i]
        for i in range(len(lista))
    ]

print("Zad 9: " + str(zamena2([1, 7, 5, 4, 9, 1, 2, 7], 5)))

# 10 ZAD

def stepen(lista):
    return [x ** y for x, y in zip(lista, lista[1:])]
