(** Testing for primality *)

open Scalable
open Scalable_basic_arithmetics
open Scalable_power

(** Deterministic primality test *)
let is_prime n = if n = [] then false else
    let n = abs_b n in
    let rec locale d n =
      if( >>) ( mult_b d d)(n ) then true
      else if mod_b n d = [] then false
      else locale (add_b [0;1] d ) n
    in locale [0;0;1] n;;

(** Pseudo-primality test based on Fermat's Little Theorem
    @param p tested bitarray
    @param testSeq sequence of bitarrays againt which to test
 *)
let is_pseudo_prime p test_seq = let rec locale p l = match l with
    [] -> true
  | e::s when (mod_power e (diff_b p [0; 1])p)=[0;1] || (mod_power e (diff_b p [0;1]) p) = [] -> (locale p s)
  | _ -> false
                                 in locale p test_seq ;;
