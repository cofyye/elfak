from collections import defaultdict

class Graph:
    def __init__(self):
        self.graph = defaultdict(list)
        
    def add_edge(self, u, v):
        self.graph[u].append(v)
        self.graph[v].append(u)

    def find_paths_of_length(self, start, end, path_length):
        def dfs(current, destination, path, current_length):
            if current == destination and current_length == path_length:
                paths.append(path.copy())
                return
            
            if current_length > path_length:
                return
            
            for neighbor in self.graph[current]:
                if neighbor not in path:
                    path.append(neighbor)
                    dfs(neighbor, destination, path, current_length + 1)
                    path.pop()
                    
        paths = []
        dfs(start, end, [start], 0)
        return paths

g = Graph()
g.add_edge(1, 2)
g.add_edge(2, 3)
g.add_edge(1, 3)
g.add_edge(3, 4)

paths = g.find_paths_of_length(1, 3, 2)
print("Pronadjeni putevi duzine 2 izmedju cvorova 1 i 3:", paths)