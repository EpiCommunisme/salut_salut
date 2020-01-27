(** Power function implementations for built-in integers *)
(*
open Builtin
open Basic_arithmetics
 *)
(* Naive and fast exponentiation ; already implemented in-class.
 *)

(** Naive power function. Linear complexity
    @param x base
    @param n exponent
 *)
let rec pow x n = match n with
    0 -> 1
  | 1 -> x
  | _ -> x * (pow x (n-1));;

(** Fast integer exponentiation function. Logarithmic complexity.
    @param x base
    @param n exponent
 *)
let rec power x n = match n with
    0 -> 1
  | 1 -> x
  | _ when n mod 2 = 0 -> power (x*x) (n/2)
  | _ -> x * power (x*x) ((n-1)/2);;

(* Modular expnonentiation ; modulo a given natural number smaller
   max_int we never have integer-overflows if implemented properly.
 *)

(** Fast modular exponentiation function. Logarithmic complexity.
    @param x base
    @param n exponent
    @param m modular base
*)

let rec mod_power x n m = if x = 0 then 0 else
    let rec pow x n m =
      let xmod = modulo x m in
      match n with
    n when n < 0 -> 0
  | 0 -> 1
  | 1 -> xmod
  | n when n mod 2 = 0 -> pow(modulo (xmod*xmod) m) (n/2) m
  | _ -> modulo (xmod * pow xmod (n-1) m) m
    in pow x n m;;


(*let mod_power x n m = if n<0 then invalid_arg "zut c casse" else
    let rec mod_rec a b m c = match b with
        0 -> c
      | _ -> mod_rec a (b-1) m (modulo (a*c) m)
    in mod_rec x n m 1;;*)


(* Making use of Fermat Little Theorem for very quick exponentation
   modulo prime number.
 *)

(** Fast modular exponentiation function mod prime. Logarithmic complexity.
    It makes use of the Little Fermat Theorem.
    @param x base
    @param n exponent
    @param p prime modular base
*)

let prime_mod_power x n p =  match x with
    0 -> 0
  | _ -> mod_power x (modulo n (p-1)) p;;
