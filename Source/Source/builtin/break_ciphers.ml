(** Factoring Built-In Int Primes *)

open Builtin
open Basic_arithmetics

(** Factors product of two primes.
    @param key is public key of an RSA cryptosystem.
 *)
let break key = let (n,p) = key in
                let rec brec i = match i with
                    _ when i > n -> (0,0)
                  | _ when n mod i = 0 -> (i,n/i)
                  | _ -> brec (i+1)
                in brec 2;;
