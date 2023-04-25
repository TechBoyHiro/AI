from copy import deepcopy
from time import time
import sys
import random


MAX_LEVEL = 5  # 4 for faster action
MAX_TIME = 10  # None


class Node:
    def __init__(self, board = None, parent=None):
        self.board = board
        self.parent = parent
        self.extreme_utility = None
        self.from_cell = None
        self.to_cell = None

    def __lt__(self, other):
        return self.extreme_utility < other.extreme_utility

    def __str__(self):
        return '\n'.join([' '.join(row) for row in self.board.board]) + '\n'


class Package:
    def __init__(self):
        self.Node = Node()
        self.Eval = None


class Tree:

    def __init__(self, board, color, opponentColor, start_time, time):
        self.root = Node(board)
        self.max_level = None
        self.color = color
        self.opponentColor = opponentColor
        self.start_row, self.end_row = (0, 5) if self.color == 'W' else (5, 0)
        self.dir = 1 if self.start_row < self.end_row else -1
        self.start_time = start_time
        self.time = time
        self.max_level = None
        self.level =0
        self.parent = None


    def MiniMax_Alpha_Beta_Search(self, parent):
        self.level = 0
        self.parent = parent
        #print("WE GET TO MINIMAX METHOD")
        Choosen_Move = self.MAX_VALUE(parent,-sys.maxunicode,sys.maxunicode)
        generatio =0
        temp = Choosen_Move.Node
        while(temp.parent != parent):
            temp = temp.parent
            generatio +=1
        if(generatio >0):
            while(generatio != 0):
                Choosen_Move.Node = Choosen_Move.Node.parent
                generatio -=1
        print("MINIMAX Comes in " + str(generatio) + " th of parent")
        print("WE DONEEEEEEEEEEEE")
        print(Choosen_Move.Node.from_cell,Choosen_Move.Node.to_cell)
        return Choosen_Move.Node.from_cell,Choosen_Move.Node.to_cell


    def MAX_VALUE(self,state, Alpha, Beta):
        if(self.CUT_OFF(state)):
            #print("We Get into Condition")
            return self.EVAL(state,1)
        #print("WE Pass Checking in MAXI")
        u = Package
        u.Node = None
        u.Eval = -sys.maxunicode
        for item in self.SUCCESSOR(state):
            if(item.parent == self.parent):
                if(self.CheckWinning(item.board)):
                    u.Node = item
                    return u
            #print("We Entryied a Maxi")
            #g = False
            #for j in range(item.board.n_rows):
                #print(item.board.board[j][0] +" "+ item.board.board[j][1] +" "+ item.board.board[j][2] +" "+ item.board.board[j][3] +" "+ item.board.board[j][4] +" "+ item.board.board[j][5])
            #if(g):
                #print("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH  ",g)
                #return
            self.level += 1
            temp = self.MIN_VALUE(item,Alpha,Beta)
            #print("We passed {} Maxi",self.level)
            if(temp.Eval > u.Eval):
                u.Eval = temp.Eval
                u.Node = temp.Node
            if(u.Eval >= Beta):
                if (state == self.parent):
                    return u
                else:
                    u.Node = state
                    return u
            if(u.Eval > Alpha):
                Alpha = u.Eval
        if(state == self.parent):
            return u
        else:
            u.Node = state
            return u


    def MIN_VALUE(self,state,Alpha,Beta):
        if(self.CUT_OFF(state)):
            return self.EVAL(state,0)
        #print("We passed checking in mini")
        u = Package
        u.Node = None
        u.Eval = sys.maxunicode
        for item in self.SUCCESSOR(state):
            #print("WE Entries a MINI")
            self.level += 1
            temp = self.MAX_VALUE(item,Alpha,Beta)
            #print("We passed {} Maxi", self.level)
            if(temp.Eval < u.Eval):
                u.Eval = temp.Eval
                u.Node = temp.Node
            if(u.Eval <= Alpha):
                u.Node = state
                return u
            if(u.Eval < Beta):
                Beta = u.Eval
        u.Node = state
        return u


    def CheckWinning(self,board):
        for j in range(board.n_cols):
            if(board.board[0][j] == self.color):
                return True
        return False



    def CUT_OFF(self,state):
        #print(self.level)
        #print(self.max_level)
        if(int(time()) >= self.start_time):
            print("THE TIME IS OVER LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL")
            return True
        if(self.level >= self.max_level):
            return True
        return state.board.finishedGame()


    def EVAL(self,state,m):
        #print("We Are In Eval")
        temp = Package()
        # Number Of mans with Weight Of 9
        Number_Of_Man = (self.CalcuteNumberOfMans(state.board) * 7)
        # Number of Enemies
        Number_Of_Enemies = (self.CalcuteNumberOfEnemis(state.board) * -7)
        # Distance Of Farest Man with Weight Of 9
        Man_Distance_From_End = (self.CalcuteDistance(state.board) * 2)
        # First Line Checking With Weight of 20
        First_Line = (self.FirstLineChecking(state.board) * 7)
        # In Attack OR Danger
        if(m == 0):
            AORD = (self.AORD(state.board) * -8)
        else:
            AORD = (self.AORD(state.board) * 8)
        # Reski with the Weight of 20
        Reski = (self.Reski(state.board) * 2)
        #### Calcuting Total Evaluation
        temp.Eval = (Number_Of_Man + Number_Of_Enemies + Man_Distance_From_End + AORD + First_Line + Reski)
        #print("The Evaluation Number Is ",temp.Eval)
        temp.Node = state
        return temp
        # Evaluation Function

    def Reski(self, board):
        second_line = 0
        first_line = 0
        for j in range(board.n_cols):
            if (board.board[3][j] == self.opponentColor):
                second_line += 1
        for i in range(board.n_cols):
            if (board.board[4][i] == self.opponentColor):
                first_line += 1
        return ((second_line * -20) + (first_line * -30))


    def AORD(self, board):
        i = board.n_rows - 1
        Number = 0
        while (i >= 1):
            for j in range(board.n_cols):
                if (board.board[i][j] == self.color):
                    if (j <= board.n_cols - 2 & j > 0):
                        if (board.board[i - 1][j + 1] == self.opponentColor):
                            Number += 1
                        elif (board.board[i - 1][j - 1] == self.opponentColor):
                            Number += 1
                    else:
                        if (j == board.n_cols - 1):
                            if (board.board[i - 1][j - 1] == self.opponentColor):
                                Number += 1
                        else:
                            if (board.board[i - 1][j + 1] == self.opponentColor):
                                Number += 1
            i -= 1
        return Number

    def FirstLineChecking(self, board):
        temp = 0
        for j in range(board.n_cols):
            if (board.board[board.n_rows - 1][j] == self.color):
                temp += 1
        # if(temp == board.n_cols):
        #   return (temp*2)
        return temp

    def CalcuteDistance(self, board):
        first_line = 0
        second_line = 0
        rest = 0
        for k in range(int(board.n_rows)):
            for w in range(board.n_cols):
                if (k == 0):
                    if (board.board[k][w] == self.color):
                        first_line += 1
                elif (k == 1):
                    if (board.board[k][w] == self.color):
                        second_line += 1
                else:
                    if (board.board[k][w] == self.color):
                        rest += 1

        return ((first_line * 40) + (second_line * 30) + (rest * 10))


    def CalcuteNumberOfMans(self, board):
        temp = 0
        for i in range(board.n_rows):
            for j in range(board.n_cols):
                if (board.board[i][j] == self.color):
                    temp += 1
        return temp

    def CalcuteNumberOfEnemis(self, board):
        temp = 0
        for i in range(board.n_rows):
            for j in range(board.n_cols):
                if (board.board[i][j] == self.opponentColor):
                    temp += 1
        return temp

    def SUCCESSOR(self,state):
        Children = []
        whose_turn = self.color if self.level % 2 == 0 else self.opponentColor
        from_cells, to_sets_of_cells = state.board.getPiecesPossibleLocations(whose_turn)
        for i, from_cell in enumerate(from_cells):
            to_set_of_cells = to_sets_of_cells[i]
            for to_cell in to_set_of_cells:
                new_board = deepcopy(state.board)
                new_board.changePieceLocation(whose_turn, from_cell, to_cell)
                child = Node(new_board, state)
                child.from_cell = from_cell
                child.to_cell = to_cell
                self.Print_Board(child)
                Children.append(child)
        return Children



    def Print_Board(self,state):
        for i in range(state.board.n_rows):
            print(state.board.board[i][0]+ " " + state.board.board[i][1]+ " " + state.board.board[i][2]+ " " + state.board.board[i][3]+ " " + state.board.board[i][4]+ " " + state.board.board[i][5])
        print("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@")



    def time_is_over(self):
        return self.time is not None and time() >= self.start_time + self.time



    # return best move (from cell, to cell), for example: ((4, 0), (3, 1))
    def move(self,Max_Level):
        self.max_level = Max_Level      # DEPTH CUT_OFF VALUE
        temp = 0
        while temp <= (self.max_level/2) and not self.time_is_over():
            print("We Get HERE :)")
            from_cell, to_cell = self.MiniMax_Alpha_Beta_Search(self.root)
            temp += 1
        return from_cell, to_cell


class Agent:
    def __init__(self, color, opponentColor, time):
        self.color = color
        self.opponentColor = opponentColor
        self.time = time

    def move(self, board):
        start_time = int(time()) + self.time
        print("The Time Is $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$   ",start_time)
        tree = Tree(board, self.color, self.opponentColor, start_time, self.time)
        return tree.move(31)
