(** Testing for primality *)

open Builtin
open Basic_arithmetics
open Power

(** Deterministic primality test *)

let nbdiv x =
  let rec nbdiv_rec x y acc = match y with
      1 -> acc
    | _ when x mod y = 0 -> nbdiv_rec x (y-1) (acc+1)
    | _ -> nbdiv_rec x (y-1) acc
  in nbdiv_rec x x 1;;

let rec is_prime n = if n = 0 then false else
  nbdiv n = 2;;

(** Pseudo-primality test based on Fermat's Little Theorem
    @param p tested integer
    @param testSeq sequence of integers againt which to test
*)

let is_pseudo_prime p test_seq = match test_seq with
    [] -> invalid_arg "nul"
  | _ -> let rec psprime p test_seq = match test_seq with
      [] -> true
      | e1::s1 when (mod_power e1 (p-1) p = 1) || ((modulo e1 p) = 0) -> psprime p s1
      | _ -> false
         in psprime p test_seq;;
