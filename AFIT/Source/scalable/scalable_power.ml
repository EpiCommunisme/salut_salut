(** Power function implementations for bitarrays *)

open Scalable
open Scalable_basic_arithmetics

(* Naive and fast exponentiation ; already implemented in-class in the
   built-in integer case.
*)

(** Naive power function. Linear complexity
    @param x base, a bitarray
    @param n exponent, a non-negative bitarray
*)

let rec yess bA bB = match (bA,bB) with
    ([],[])-> true
  | ([],e::s) when e = 0 -> yess bA s
  | (e::s,[]) when e = 0 -> yess s bB
  | (e::s,e2::s2) when e = 0 && e2 = 0 -> yess s s2
  | _ -> false;;

let pow x n = if x =[] then [] else let rec powr x n = if n = [] then [0;1]
  else mult_b x (powr x (diff_b n [0;1]))
in powr x n;;

(** Fast bitarray exponentiation function. Logarithmic complexity.
    @param x base, a bitarray
    @param n exponent, a non-negative bitarray
*)
let power x n = if x =[] then [] else
    let rec power_r x n = match n with
    [] -> [0;1]
  | [0;1] -> x
  | _ when mod_b n [0;0;1] = [] -> mult_b (power_r x (quot_b n [0;0;1])) (power_r x (quot_b n [0;0;1]))
  | _ -> mult_b x (mult_b (power_r x (quot_b n [0;0;1])) (power_r x (quot_b n [0;0;1])))
       in power_r x n ;;

(* Modular expnonentiation ; modulo a given natural (bitarray without
   sign bits).
*)

(** Fast modular exponentiation function. Logarithmic complexity.
    @param x base, a bitarray
    @param n exponent, a non-negative bitarray
    @param m modular base, a positive bitarray
 *)
let mod_power x n m = mod_b (power x n) m;;

(* Making use of Fermat Little Theorem for very quick exponentation
   modulo prime number.
 *)

(** Fast modular exponentiation function mod prime. Logarithmic complexity.
    It makes use of the Little Fermat Theorem.
    @param x base, a bitarray
    @param n exponent, a non-negative bitarray
    @param p prime modular base, a positive bitarray
 *)
let prime_mod_power x n p = if yess x [0] then [] else mod_power x (mod_b n (diff_b p [0;1])) p ;;
