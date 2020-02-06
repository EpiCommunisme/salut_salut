(** Factoring bitarrays into primes *)

open Scalable
open Scalable_basic_arithmetics

(** Factors product of two prime bitarrays.
    @param key is public key of an RSA cryptosystem.
 *)
let break key = let (n,e) = key
  in if mod_b n [0;0;1] =[] then  ( [0;0;1], quot_b n  [0;0;1])
    else let rec break_r n [0;1] = if mod_b n [0;1] = [] then ([0;1],quot_b n [0;1])
      else break_r n (add_b [0;1] [0;0;1])
         in break_r n (from_int 3);;
