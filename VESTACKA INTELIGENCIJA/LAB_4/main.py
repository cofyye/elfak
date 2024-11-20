def print_board(board, pawn_symbol='*'):
    n = len(board)
    for row in range(n):
        line = ['.'] * n
        line[board[row]] = pawn_symbol
        print(' '.join(line))
    print()


def solve_pawns(initial_board):
    n = len(initial_board)
    board = [-1] * n
    moves_log = []

    def is_safe(board, row, col):
        for i in range(row):
            if board[i] == col:
                return False
        return True

    def place_pawns(board, row):
        if row == n:
            return True

        for col in range(n):
            if is_safe(board, row, col):
                previous_position = initial_board[row]
                board[row] = col
                moves_log.append(
                    f"Pion u redu {row+1} pomeren sa kolone {previous_position+1} na kolonu {col+1}"
                )
                if place_pawns(board, row + 1):
                    return True
                moves_log.pop()

        return False

    print("Pocetno stanje tabele:")
    print_board(initial_board, pawn_symbol='*')

    if place_pawns(board, 0):
        print("Konacno stanje tabele:")
        print_board(board, pawn_symbol='*')
        print("Logovi pomeranja:")
        for move in moves_log:
            print(move)
    else:
        print("Nije moguce resiti problem")

initial_board = [3, 2, 3, 3, 7, 6, 2, 1]
solve_pawns(initial_board)