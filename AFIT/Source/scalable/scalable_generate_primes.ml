(** Generating prime bitarrays *)

open Scalable
open Scalable_basic_arithmetics

(* Initializing list of bitarrays for eratosthenes's sieve. Naive
   version.
*)

(** List composed of 2 and then odd bitarrays starting at 3.
    @param n upper bound to elements in the list of bitarrays.
 *)
let init_eratosthenes n = let n = from_int n in if (<<) n [0;1] then [[0;0;1]] else
  let rec locale l a = match a with
      _ when (>>) a n -> []
    | [0;0;1] -> a:: locale l(add_b a [0;1])
    | _ when mod_b a [0;0;1] = [] -> locale l(add_b a [0;1])
    | _ -> a::locale l(add_b a [0;1])
       in locale [] [0;0;1];;

(* Eratosthenes sieve. *)

(** Eratosthene sieve.
    @param n upper bound to elements in the list of primes, starting
           at 2.
*)
let eratosthenes n = let rec foo i l = match l with
    [] -> []
  | e::s -> if mod_b e i = [] then foo i s else e::(foo i s)
                     in let rec foo_bis l = match l with
                         [] -> []
                       | e::s -> e::(foo_bis(foo e s))
               in foo_bis (init_eratosthenes n);;

(* Write and read into file functions for lists. *)

(** Write a list into a file. Element seperator is newline. Inner
   seperator within elements of an element is ','.
   @param file path to write to.
*)
let write_list li file = let ope = open_out file in let rec locale li = match li with
    [] -> close_out ope
  | e::s -> (Printf.fprintf ope "%d\n" e; locale s)
                                                    in locale li ;;

(** Write a list of prime numbers up to limit into a txt file.
    @param n limit of prime bitarrays up to which to build up a list of primes.
    @param file path to write to.
*)
let write_list_primes n file = ()

(** Read file safely ; catch End_of_file exception.
    @param in_c input channel.
 *)
let input_line_opt in_c =
  try Some (input_line in_c)
  with End_of_file -> None

(** Create a list of bitarrays out of reading a line per line channel.
    @param in_c input channel.  *)
let rec create_list in_c = match input_line_opt in_c with
    Some s -> (int_of_string s)::(create_list in_c)
  | None -> (close_in in_c ; []) ;;

(** Load list of prime bitarrays into OCaml environment.
    @param file path to load from.
 *)
let read_list_primes file = []

(* Auxiliary functions to extract big prime numbers for testing
   purposes.
 *)

(** Get last element of a list.
    @param l list of prime bitarrays.
 *)
let rec last_element l = match l with
  | [] -> failwith "Scalable.generate_primes.last_element: Youre list \
                    is empty. "
  | e::[] -> e
  | h::t -> last_element t

(** Get two last elements.
    @param l list of prime bitarrays.
 *)
let rec last_two l = match l with
  | [] | [_] -> failwith "Scalable.generate_primes.last_two: List has \
                          to have at least two elements."
  | e::g::[] -> (e, g)
  | h::t -> last_two t
;;

(* Generating couples of prime bitarrays for specific or fun
   purposes.
 *)

(** Finding couples of prime bitarrays where second entry is twice the
    first plus 1.
    @param upper bound for searched for prime bitarrays, a built-in integer.
    @param isprime function testing for (pseudo)primality.  *)

let is_prime n = if n = [] then false else
    let n = abs_b n in
    let rec locale d n =
      if( >>) ( mult_b d d)(n ) then true
      else if mod_b n d = [] then false
      else locale (add_b [0;1] d ) n
    in locale [0;0;1] n;;

let double_primes limit isprime =
  let l = eratosthenes(limit) in
  let rec locale l l2 = match l with
      []->l2
    | e::s when is_prime(add_b(mult_b [0;0;1] e)[0;1])->(e,(add_b(mult_b [0;0;1] e)[0;1]))::locale s l2
    | _::s -> locale s l2
  in locale l [];;

(** Finding twin primes.
    @param upper bound for searched for prime bitarrays, a built-in integer..
    @param isprime function testing for (pseudo)primality.
 *)
let twin_primes limit isprime =
  let l = eratosthenes(limit) in
  let rec locale l l2 = match l with
      []->l2
    | e::s when is_prime(add_b e [0;0;1])->(e,(add_b e [0;0;1]))::locale s l2
    | _::s -> locale s l2
  in locale l [];;
