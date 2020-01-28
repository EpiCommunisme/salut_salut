(** Basic arithmetics for ordered euclidian ring. *)

open Scalable

(** Greater common (positive) divisor of two non-zero integers.
    @param bA non-zero bitarray.
    @param bB non-zero bitarray.
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

let rec gcd a b = match (a,b) with
    (a,b) when a < 0 -> gcd (-a) b
  | (a,b) when b < 0 -> gcd a (-b)
  | _ -> let rec gdc_rec a b = match (div a b) with
      (a,0) -> gcd_rec b d
	 in gcd_rec a b;;

let gcd_b bA bB = []
  bA = to_int bA
  bB = to_int bB
  x = gcd bA bB
  

(** Extended euclidean division of two integers NOT OCAML DEFAULT.
    Given non-zero entries a b computes triple (u, v, d) such that
    a*u + b*v = d and d is gcd of a and b.
    @param bA non-zero bitarray.
    @param bB non-zero bitarray.
*)
let bezout_b bA bB = ([], [], [])
