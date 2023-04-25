


persons(0,[]) :- !.
persons(N,[(_sb,_tovote,_brotherof,_fatherof,_boss,_numofvote)|T]) :- 
N1 is N-1,persons(N1,T).


person(1,[H|_],H):- !.
person(N,[_|T],R) :- N1 is N-1,person(N1,T,R).

%tokyo's vote are more than capetown
hint1([(tokyo,_,_,_,_,X),(capetown,_,_,_,_,Z)|_]) :- X>Z.
hint1([(capetown,_,_,_,_,Z),(tokyo,_,_,_,_,X)|_]) :- Z<X.
hint1([_|T]) :- hint1(T).

%Rio Vote Tokyo
hint2([(rio,tokyo,_,_,_,_)|_]).
hint2([_|T]):- hint2(T).

%third low
hint3([(X,Y,_,_,_,_)|_]):- X=/=Y,=/=hint3([(_,X,_,_,_,_)|_]).
hint3([_|T]):- hint3(T).

%forth low
hint4([(tokyo,_,_,_,_,Z),(helsinki,_,_,_,_,W)|_]) :- Z =/= W.
hint4([_|T]):- hint4(T).

%fifth low
hint5((montreal,_,_,_,_,Z),[(_,_,_,_,_,Q)|T]):- 
Z>Q ; Z<Q ,hint5((montreal,_,_,_,_,Z),T).
hint5([_|T]):- hint5(T).

%sixeth low
hint6([(denver,moscow,_,_,_,_)|_]).
hint6([_|T]):-hint6(T)

hint7([(moscow,_,_,denver,_,_)|_]).
hint7([_|T]):-hint7(T).

%4 or 2 or 2
hint8([(_,_,_,_,_,0)|_]).
hint8([(_,_,_,_,_,2)|_]).
hint8([(_,_,_,_,_,2)|_]).
hint8([(_,_,_,_,_,4)|_]).
hint8([(_,_,_,_,_,1)|_]).

% two last last low
hint9([(_,_,_,_,_,0)|_]).
hint9([(_,_,_,_,_,2)|T]):-hint9(T).

%previous boss couldnt vote himself
hint10([(W,_,_,_,W,_)|_]):- =/=hint10([(W,W,__,W,_)|_]).
hint10([_|T]):-hint10(T).

%helinski and oslo are brothers
hint11([(helsinki,_,oslo,_,_,_)|_]).
hint11(_|T):-hint11(T).

%capetown has one vote lower than exact one brothers
hint12([(capetown,_,_,_,_,Z),(W,_,Q,_,_,S)|_]):- Z<S-1.
hint12([_|T]):-hint12(T).

hint13([(berlin,_,_,_,berlin,_)|_]).
hint13([_|T]):-hint13(T).

% how many vote does capetown have ?
question([(capetown,_,_,_,_,D)|_]).
question([_|T]):- question(T).

solution(Persons):- persons(10,Persons),hint1(Persons),hint2(Persons),
hint3(Persons),hint4(Persons),hint5(Persons),hint6(Persons),
hint7(Persons),hint8(Persons),hint9(Persons),hint10(Persons),
hint11(Persons),hint12(Persons),hint13(Persons),guestion(Persons).






 


