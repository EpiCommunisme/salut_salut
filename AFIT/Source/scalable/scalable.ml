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

let rec invert l = match l with
    [] -> []
  | e::s when e = 1 -> 0::invert s
  | e::s -> 1::invert s;;

let rec from_int x =
  let sign_bit = if x < 0 then 1 else 0 in
  let rec from_int_r x rem =
    if x == 0 then rem::[] else
      rem::(from_int_r (x/2) (x mod 2)) in
  sign_bit::(from_int_r (abs x/2) (abs x mod 2));;

(** Transforms bitarray of built-in size to built-in integer.
    UNSAFE: possible integer overflow.
    @param bA bitarray object.
*)

let rec power a = function
    0 -> 1
  | i -> a*(power a (i-1));;

let to_int bA =
  let (sign, rest) = match bA with
      [] -> (0,[])
    | 0::s -> (1,s)
    | _::s -> ((-1),s) in
  let rec to_int_r array deg = match array with
      [] -> 0
    | e::s -> e*(power 2 deg) + (to_int_r s (deg+1))
  in sign*(to_int_r rest 0);;

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

let reverse l =
  let rec rev l l2 = match l with
    |[] -> l2
    |a::r -> rev r (a::l2)
  in rev l [];;

let rec shorten l = match l with
  |[] -> []
  |0::r -> shorten r
  |a::r -> a::r;;

let rec compare_n nA nB =
  let rec comparebis a b = match (a , b) with
     |([] ,[]) -> 0
     |([],0::r) -> -1
     |([],1::r) -> -1
     |(0::r,[]) -> 1
     |(1::r,[]) -> 1
     |(0::r , 0::r1) -> comparebis r r1
     |(1::r , 1::r1) -> comparebis r r1
     |(0::r , 1::r1) -> -1
     |(1::r , 0::r1) -> 1
     |(_,_) -> invalid_arg ("not binary")
    in comparebis (shorten (reverse nA)) (shorten (reverse nB));;

(** Bigger inorder comparison operator on naturals. Returns true if
    first argument is bigger than second and false otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (>>!) nA nB = compare_n nA nB == 1;;

(** Smaller inorder comparison operator on naturals. Returns true if
    first argument is smaller than second and false otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (<<!) nA nB = compare_n nA nB == -1;;

(** Bigger or equal inorder comparison operator on naturals. Returns
    true if first argument is bigger or equal to second and false
    otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (>=!) nA nB = compare nA nB == 1 || compare_n nA nB == 0;;

(** Smaller or equal inorder comparison operator on naturals. Returns
    true if first argument is smaller or equal to second and false
    otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (<=!) nA nB = compare_n nA nB == -1 || compare_n nA nB == 0;;

(** Comparing two bitarrays. Output is 1 if first argument is bigger
    than second -1 if it smaller and 0 in case of equality.
    @param bA A bitarray.
    @param bB A bitarray.
*)

let rec comp1 l = match l with
    |[] -> []
    |1::r -> 0::(comp1 r)
    |0::r -> 1::(comp1 r)
    |_::r -> invalid_arg "non binaire";;

let rec add1 l = match l with
  |[] -> []
  |0::r -> 1::r
  |1::r -> 0::(add1 r)
  |_::r -> invalid_arg "non binaire";;



let compare_b bA bB =
  let rec compb a b = match (a,b) with
    |([],[]) -> 0
    |(0::r,0::r2) -> compare_n (reverse a) (reverse b)
    |(1::r,1::r2) -> compare_n (add1 (comp1 (reverse b))) (add1 (comp1 (reverse a)))
    |(1::r, 0::r2) -> -1
    |(0::r, 1::r2) -> 1
    |(_,_) -> invalid_arg "non binaire"
  in compb (reverse bA) (reverse bB) ;;

(** Bigger inorder comparison operator on bitarrays. Returns true if
    first argument is bigger than second and false otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (<<) bA bB = compare_b bA bB == (-1);;

(** Smaller inorder comparison operator on bitarrays. Returns true if
    first argument is smaller than second and false otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (>>) bA bB = compare_b bA bB == 1;;

(** Bigger or equal inorder comparison operator on bitarrays. Returns
    true if first argument is bigger or equal to second and false
    otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (<<=) bA bB = compare_b bA bB == (-1) || compare_b bA bB == 0;;

(** Smaller or equal inorder comparison operator on naturals. Returns
    true if first argument is smaller or equal to second and false
    otherwise.
    @param nA natural.
    @param nB natural.
 *)
let (>>=) bA bB = compare_b bA bB == 1 || compare_b bA bB == 0;;
;;

(** Sign of a bitarray.
    @param bA Bitarray.
*)
let sign_b bA =
  let rec signb a = match a with
    |[] -> 1
    |1::r -> (-1)
    |0::r -> 1
    |_ -> invalid_arg "non binaire"
  in signb (reverse bA) ;;

(** Absolute value of bitarray.
    @param bA Bitarray.
*)
let abs_b bA =
  let rec absb a = match a with
    |[] -> []
    |1::r ->add1 (reverse (comp1 a))
    |0::r -> a
    |_ -> invalid_arg "non binaire"
  in absb (reverse bA) ;;

(** Quotient of integers smaller than 4 by 2.
    @param a Built-in integer smaller than 4.
*)
let _quot_t a = 0

(** Modulo of integer smaller than 4 by 2.
    @param a Built-in integer smaller than 4.
*)
let _mod_t a = 0

(** Division of integer smaller than 4 by 2.
    @param a Built-in integer smaller than 4.
*)
let _div_t a = (0, 0)

(** Addition of two naturals.
    @param nA Natural.
    @param nB Natural.
*)
let add_n nA nB = []

(** Difference of two naturals.
    UNSAFE: First entry is assumed to be bigger than second.
    @param nA Natural.
    @param nB Natural.
*)
let diff_n nA nB = []

(** Addition of two bitarrays.
    @param bA Bitarray.
    @param bB Bitarray.
 *)
let add_b bA bB = []

(** Difference of two bitarrays.
    @param bA Bitarray.
    @param bB Bitarray.
*)
let diff_b bA bB = []

(** Shifts bitarray to the left by a given natural number.
    @param bA Bitarray.
    @param d Non-negative integer.
*)
let rec shift bA d = []

(** Multiplication of two bitarrays.
    @param bA Bitarray.
    @param bB Bitarray.
*)
let mult_b bA bB = []

(** Quotient of two bitarrays.
    @param bA Bitarray you want to divide by second argument.
    @param bB Bitarray you divide by. Non-zero!
*)
let quot_b bA bB = []

(** Modulo of a bitarray against a positive one.
    @param bA Bitarray the modulo of which you're computing.
    @param bB Bitarray which is modular base.
 *)
let mod_b bA bB = []

(** Integer division of two bitarrays.
    @param bA Bitarray you want to divide.
    @param bB Bitarray you wnat to divide by.
*)
let div_b bA bB = ([], [])