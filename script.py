a = []
s = ''
while 1:
    if s == '0':
        break
    s = input()
    a.append('\"' + s + '\",')

for i in a:
    print(i)
    