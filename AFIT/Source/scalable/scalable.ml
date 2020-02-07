(** A naive implementation of big integers

This module aims at creating a set of big integers naively. Such data
types will be subsequently called bitarrays. A bitarray is a list of
zeros and ones ; first integer representing the sign bit. In this
context zero is reprensented by the empty list []. The list is to
be read from left to right ; this is the opposite convention to the
one you usually write binary decompositions with. After the sign bit
the first encountered bit is the coefficient in front of two to
the power zero. This convention has been chosen to ease writing
down code. A natural bitarray is understood as being a bitarray of
which you've taken out the sign bit, it is just the binary
decomposition of a non-negative integer.

 *)

(** Creates a bitarray from a built-in integer.
    @param x built-in integer.
*)

let yes bA = (*test*)
  let a = match bA with
      [] ->bA
    | _::s -> s
  in let rec foo a = match a with
      []-> true
    | e::s when e = 0 -> foo  s
    | _ -> false
     in foo a;;

let nul x = (*zero*)
  let rec foo c x = match x with
    []->c
  | e::s when e = 1 -> 1+foo c s
  | e::s -> foo c s
  in let rec foobis c x res = match (c,x) with
      (0,_) -> res
    | (_,e::s) when e = 1 -> e::foobis (c-1) s res
    | (_,e::s) -> e::foobis (c) s res
    | _ -> res
     in if yes(foobis(foo 0 x)x []) then [] else (foobis (foo 0 x) x []);;

let from_int x = match x with
    0 -> []
  | _ -> let a = match x with
      _ when x >= 0 -> 0
      | _ -> 1 in let rec foo x l = match x with
          0 -> l
        | _ -> abs((x mod 2))::foo (x/2)(l)
                  in let plus = a::foo x []
                     in plus ;;

(** Transforms bitarray of built-in size to built-in integer.
    UNSAFE: possible integer overflow.
    @param bA bitarray object.
*)


let rec power x n = match n with
      0 -> 1
    | 1 -> x
    | _ when n mod 2 = 0 -> (power(x)(n/2))*(power(x)(n/2))
    | _ -> x*(power(x)(n/2))*(power(x)(n/2));;


let to_int bA = let a = match bA with
    []-> 1
  | e::_ -> match e with
      0 -> 1
      | _ -> -1 in let rec foo bA c = match bA with
          []-> 0
        | e::s -> e*power 2 c +foo s (c+1)
                   in let res = (foo bA 0)*a
                      in res/2 ;;

(** Prints bitarray as binary number on standard output.
    @param bA a bitarray.
  *)

let rec print_b bA = match bA with
    [] -> ()
  | e::s -> print_string (string_of_int e); print_b s;;

(** Toplevel directive to use print_b as bitarray printer.
    CAREFUL: print_b is then list int printer.
    UNCOMMENT FOR TOPLEVEL USE.
*)
(* #install_printer print_b *)

(** Internal comparisons on bitarrays and naturals. Naturals in this
    context are understood as bitarrays missing a bit sign and thus
    assumed to be non-negative.
*)

(** Comparing naturals. Output is 1 if first argument is bigger than
    second -1 if it is smaller and 0 in case of equality.
    @param nA A natural, a bitarray having no sign bit.
           Assumed non-negative.
    @param nB A natural.
*)


let to_int2 bA =
  let rec foo bA c = match bA with
      []-> 0
    | e::s -> e*power 2 c +foo s (c+1)
  in (foo bA 0);;

let rec lenght l = match l with (*taille*)
    []-> 0
  | _::s1 -> 1+lenght s1 ;;

let inv l =
  let rec foo l res = match l with
      []-> res
    | e::s -> foo s (e::res)
  in foo l [];;

let rec compare_n nA nB = if lenght nA > lenght nB then 1 else
    if lenght nA< lenght nB then -1 else
      let a = inv nA in
      let b= inv nB in
      let rec locale nA nb = match (nA,nb) with
          ([],[])-> 0
        | ((_::_, [])|([], _::_)) -> 0
        | (e1::_,e2::_) when e1>e2 -> 1
        | (e1::_,e2::_) when e1<e2 -> -1
        | (_::s1,_::s2) -> locale s1 s2
      in locale a b;;



(** Bigger inorder comparison operator on naturals. Returns true if
    first argument is bigger than second and false otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (>>!) nA nB = compare_n nA nB = 1;;

(** Smaller inorder comparison operator on naturals. Returns true if
    first argument is smaller than second and false otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (<<!) nA nB = compare_n nA nB = -1;;

(** Bigger or equal inorder comparison operator on naturals. Returns
    true if first argument is bigger or equal to second and false
    otherwise.
    @param nA natural.
    @param nB natural.
*)

let (>=!) nA nB = compare_n nA nB = 1 || compare_n nA nB = 0;;

(** Smaller or equal inorder comparison operator on naturals. Returns
    true if first argument is smaller or equal to second and false
    otherwise.
    @param nA natural.
    @param nB natural.
 *)

let (<=!) nA nB = compare_n nA nB = -1 || compare_n nA nB = 0;;

(** Comparing two bitarrays. Output is 1 if first argument is bigger
    than second -1 if it smaller and 0 in case of equality.
    @param bA A bitarray.
    @param bB A bitarray.
*)

let rec yess bA bB = match (bA,bB) with (*testz*)
    ([],[])-> true
  | ([],e::s) when e = 0 -> yess bA s
  | (e::s,[]) when e = 0 -> yess s bB
  | (e::s,e2::s2) when e=0 && e2=0 -> yess s s2
  | _ -> false;;

let compare_b bA bB = match (bA,bB) with
    (e::s,e2::s2) when e=1 && e2=1 -> if compare_n s s2 =0 then 0 else
        if  compare_n s s2 =1 then -1 else 1
  | (e::s,e2::s2) when e=0 && e=0 -> compare_n s s2
  | (e::s,e2::s2) when e=1 && e=0 ->if yess s s2 then 0 else -1
  | (e::s,e2::s2) when e=0 && e=1 ->if yess s s2 then 0 else 1
  |_ -> 0;;

(** Bigger inorder comparison operator on bitarrays. Returns true if
    first argument is bigger than second and false otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (<<) bA bB = compare_b bA bB = -1;;

(** Smaller inorder comparison operator on bitarrays. Returns true if
    first argument is smaller than second and false otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (>>) bA bB =  compare_b bA bB = 1;;

(** Bigger or equal inorder comparison operator on bitarrays. Returns
    true if first argument is bigger or equal to second and false
    otherwise.
    @param nA natural.
    @param nB natural.
*)

let (<<=) bA bB = compare_b bA bB = (-1) || compare_b bA bB = 0;;

(** Smaller or equal inorder comparison operator on naturals. Returns
    true if first argument is smaller or equal to second and false
    otherwise.
    @param nA natural.
    @param nB natural.
*)

let (>>=) bA bB = compare_b bA bB = 1 || compare_b bA bB = 0;;

(** Sign of a bitarray.
    @param bA Bitarray.
*)
let sign_b bA = match bA with
    [] -> 1
  | e::s -> if e = 1 then -1 else 1;;

(** Absolute value of bitarray.
    @param bA Bitarray.
*)
let abs_b bA = match bA with
    [] -> bA
  | e::s when sign_b bA = -1 -> 0::s
  | _ -> bA;;

(** Quotient of integers smaller than 4 by 2.
    @param a Built-in integer smaller than 4.
*)
let _quot_t a = if a > 3 then invalid_arg "Error" else a/2;;

(** Modulo of integer smaller than 4 by 2.
    @param a Built-in integer smaller than 4.
*)
  let _mod_t a = if a > 3 then invalid_arg "Error" else a mod 2;;

(** Division of integer smaller than 4 by 2.
    @param a Built-in integer smaller than 4.
*)
let _div_t a = (_quot_t a, _mod_t a);;

(** Addition of two naturals.
    @param nA Natural.
    @param nB Natural.
*)

let truc x = (*from int 2*)
  let rec locale x l = match x with
      0 -> l
    | _ ->abs( (x mod 2))::locale( x/2)(l)
  in locale x [];;

let add_n nA nB =
  let rec foo nA nB l c = match (nA,nB) with
      ([],[]) -> if c=0 then l else c::l
    | ([],e2::s2) -> if e2+c >1 then ((e2+c)mod 2)::foo nA s2 l 1 else ((e2+c)mod 2)::foo nA s2 l 0
    | (e::s,[]) -> if e+c >1 then ((e+c)mod 2)::foo s nB l 1 else ((e+c)mod 2)::foo s nB l 0
    | (e::s,e2::s2) -> if (e+e2+c)>1 then ((e+e2+c)mod 2)::foo s s2 l 1 else ((e+e2+c)mod 2)::foo s s2 l 0
  in foo nA nB [] 0 ;;

(** Difference of two naturals.
    UNSAFE: First entry is assumed to be bigger than second.
    @param nA Natural.
    @param nB Natural.
*)
let diff_n nA nB =
  let rec foo nA nB res c = match (nA,nB) with
      ([],[]) -> res
    | ([],e2::s2) -> invalid_arg "nB doit etre plus grand que nA"
    | (e::s,[]) -> if e -c >=0 then (e- c)::foo s nB res 0 else (2-c)::foo s nB res 1
    | (e::s,e2::s2) when e2+c = 2 && e=1 ->if e -(e2+c)>=0 then (e-(e2+c))::foo s s2 res 0 else (3-(e2+c))::foo s s2 res 1
    | (e::s,e2::s2) ->if e -(e2+c)>=0 then (e-(e2+c))::foo s s2 res 0 else (2-(e2+c))::foo s s2 res 1
  in foo nA nB [] 0;;

(** Addition of two bitarrays.
    @param bA Bitarray.
    @param bB Bitarray.
*)

let yahou bA bB = match (bA,bB) with (*boom*)
    ([],_) -> bB
  | (_,[]) -> bA
  | (e::s,e2::s2) when e=1 && e2=1 -> 1::add_n s s2
  | (e::s,e2::s2) when e =1->if ( (>=!)s s2) then 1::diff_n s s2 else 0::diff_n s2 s
  | (e::s,e2::s2) when e2 =1-> if ( (>=!)s s2) then 0::diff_n s s2 else 1::diff_n s2 s
  | (e::s,e2::s2) ->0::add_n s s2 ;;


let add_b bA bB = if yes (yahou bA bB) then [] else nul (yahou bA bB);;

(** Difference of two bitarrays.
    @param bA Bitarray.
    @param bB Bitarray.
*)
let diff_b bA bB = match (bA,bB) with
    (e::s,e2::s2) when e=0 && e2=0 -> add_b bA (1::s2)
  | (e::s,e2::s2) when e=0 -> add_b bA (0::s2)
  | (e::s,e2::s2) when e2=0 ->1::add_n s s2
  | (e::s,e2::s2) -> add_b bA (0::s2)
  | ((_::_, [])|([], _)) -> [];;


(** Shifts bitarray to the left by a given natural number.
    @param bA Bitarray.
    @param d Non-negative integer.
*)

let rec shift bA d = (*shift*)
  let foo bA = match bA with
      [] -> []
    | e::s -> s
  in let rec foobis bA d = match (bA,d) with
      ([],_) -> []
    | (_,0) -> bA
    | _ -> 0::foobis bA (d-1)
     in foobis (foo bA) d;;

(** Multiplication of two bitarrays.
    @param bA Bitarray.
    @param bB Bitarray.
*)

let rec mult_b bA bB = if sign_b bB=1 then
    let rec foo bA bB = if yess bB [0] then [0;0] else add_b bA (foo bA (diff_b bB (from_int 1)))
    in foo bA bB else
    let rec foobis bA bB = if yess bB [0] then [0;0] else add_b bA (foobis bA (diff_b (abs_b bB) (from_int 1)))
    in let res = foobis bA bB
       in match res with
           e::s when sign_b bA = -1 && sign_b bB = -1 ->0::s
         | e::s when sign_b bA = -1 ->res
         | e::s when sign_b bB = -1 ->1::s
         | _  -> res;;


(** Quotient of two bitarrays.
    @param bA Bitarray you want to divide by second argument.
    @param bB Bitarray you divide by. Non-zero!
*)

let bof bA bB = (*pasouf*)
  let rec foo bA c = match bA with
      [] -> c
    | _ when (<<)bB bA -> c
    | _ -> foo(add_b bA bB)(add_b c [1;1])
  in foo bA [];;

let bof_r bA bB = diff_b bA (mult_b bB(bof(bA)bB));;

let quot_b bA bB =
  let rec foo bA c = match bA with
      [] -> c
    | _ when (>>)  bB bA -> c
    | _ -> foo(diff_b bA bB)(add_b c [0;1])
  in let foobis bA = if sign_b bA = 1 then foo bA [] else
      if diff_b bA (mult_b bB (bof (bA) bB)) = []
      then mult_b (foo (abs_b bA)[])([1;1]) else
        mult_b (add_b(foo(abs_b bA)[]) [0;1])([1;1])
     in foobis bA;;

(** Modulo of a bitarray against a positive one.
    @param bA Bitarray the modulo of which you're computing.
    @param bB Bitarray which is modular base.
 *)
let mod_b bA bB = diff_b bA (mult_b bB (quot_b bA bB));;

(** Integer division of two bitarrays.
    @param bA Bitarray you want to divide.
    @param bB Bitarray you wnat to divide by.
*)
let div_b bA bB = (quot_b bA bB, mod_b bA bB);;
