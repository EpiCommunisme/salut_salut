(** Basic arithmetics with built-in integers *)

open Builtin

(* Greater common divisor and smaller common multiple
   implemetations.
*)

(** Greater common (positive) divisor of two non-zero integers.
    @param a non-zero integers
    @param b non-zero integer
*)
let rec gcd a b = match (a,b) with
    (a,b) when a < 0 -> gcd (-a) b
  | (a,b) when b < 0 -> gcd a (-b)
  | _ -> let rec gcd_rec a b = match (div a b) with
    (a,0) -> b
      | (c,d) -> gcd_rec b d
         in gcd_rec a b;;

(* Extended Euclidean algorithm. Computing Bezout Coefficients. *)

(** Extended euclidean division of two integers NOT OCAML DEFAULT.
    Given non-zero entries a b computes triple (u, v, d) such that
    a*u + b*v = d and d is gcd of a and b.
    @param a non-zero integer
    @param b non-zero integer.
*)
let bezout a b =
  let rec bezout_rec (c,d,e,c',d',e') = match e' with
      0 -> (c,d,e)
    | _ -> let f = e/e'
           in bezout_rec(c',d',e',c-f*c',d-f*d',e-f*e')
  in bezout_rec(1,0,a,0,1,b);;
